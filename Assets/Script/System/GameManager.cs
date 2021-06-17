using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Public;

public class GameManager : CSigleton<GameManager>
{
    [SerializeField] private int _NumOfLoadAsync;
    public int NumOfLoadAsync
    {
        get => _NumOfLoadAsync;
        set
        {
            if (value < 0 || value == _NumOfLoadAsync) return;
            if (value == 0)
            {
                _NumOfLoadAsync = -1;
                CSceneManager.Instance.LoadLevel(1);
            }
            _NumOfLoadAsync = value;
        }
    }

    protected override void Awake()
    {
        base.Awake();
        _NumOfLoadAsync = 0;
    }
}
