using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy/Default")]
public class EnemyData : ScriptableObject
{
    public string Name;
    public string Description;

    public Sprite Sprite;

    public bool IsReward;
    public bool IsEnemy;

    public int Attack;
    public int Defence;
    public int Health;
    public int Value;

    public float MoveSpeed ;
    public Vector2 DirectionMove;

    [CustomEditor(typeof(TestComponent))]
    [CanEditMultipleObjects]

    public class TestComponentEditor : Editor
    {
        TestComponent subject;
        SerializedProperty compType;
        SerializedProperty compName;
        SerializedProperty varCompFirst;
        SerializedProperty varCompSecond;

        //Передаём этому скрипту компонент и необходимые в редакторе поля
        void OnEnable()
        {
            subject = target as TestComponent;

            compType = serializedObject.FindProperty("component");
            compName = serializedObject.FindProperty("componentName");

            varCompFirst = serializedObject.FindProperty("variableComponentFirst");
            varCompSecond = serializedObject.FindProperty("variableComponentSecond");
        }

        //Переопределяем событие отрисовки компонента
        public override void OnInspectorGUI()
    {
        //Метод обязателен в начале. После него редактор компонента станет пустым и
        //далее мы с нуля отрисовываем его интерфейс.
        serializedObject.Update();

        //Вывод в редактор выпадающего меню
        EditorGUILayout.PropertyField(compType);
        //Вывод в редактор текстового поля
        EditorGUILayout.PropertyField(compName);
        //Изменение значения текстового поля
        compName.stringValue = "None";

        //Проверка выбранного пункта в выпадающем меню, 
        if (subject.component == TestComponent.ComponentType.First)
        {
            //Вывод в редактор слайдера
            EditorGUILayout.IntSlider(varCompFirst, 0, 100, new GUIContent("Variable First"));
            compName.stringValue = "First";

        }
        else if (subject.component == TestComponent.ComponentType.Second)
        {
            EditorGUILayout.IntSlider(varCompSecond, 0, 100, new GUIContent("Variable Second"));
            compName.stringValue = "Second";
        }

        //Метод обязателен в конце
        serializedObject.ApplyModifiedProperties();
    }
    private void OnEnable()
    {
        if (IsReward && IsEnemy)
        {
            IsReward = false;
            Debug.Log("Объект " + Name + " не может быть и наградой и врагом одновременно, значение Is Reward = false");
        }
        if (IsReward)
        {
            Attack = 0;
            Defence = 0;
            Health = 0;
        }
        if (IsEnemy)
        {
            Value = 0;
        }
    }
}
