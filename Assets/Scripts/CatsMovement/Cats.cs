using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Cats : MonoBehaviour
{
    private Dictionary<Type, ICatBehavior> _behaviorsMap;
    private ICatBehavior _currentBehavior;

   


    private Vector3 _targetPositions;
    private NavMeshAgent _agent;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
    }


    private void Start()
    {
        this.InitBehaviors();
        this.SetBehaviorByDefault();
    }


    private void Update()
    {
        if(this._currentBehavior != null)
        {
            this._currentBehavior.Update();
        }
        SetTargetPositions();
        SetAgentPosition();
    }

    private void InitBehaviors()
    {
        this._behaviorsMap = new Dictionary<Type, ICatBehavior>();

        this._behaviorsMap[typeof(CatBehFishing)] = new CatBehFishing();
        this._behaviorsMap[typeof(CatBehWorckOnSellersShop)] = new CatBehWorckOnSellersShop();
    }


    private void SetBehavior(ICatBehavior newBehavior)
    {
        if(this._currentBehavior != null) 
        {
            this._currentBehavior.Exit();
        }

        this._currentBehavior = newBehavior;
        this._currentBehavior.Enter();
    }

    private ICatBehavior GetBehavior<T>() where T : ICatBehavior
    {
        var type = typeof(T);
        return  this._behaviorsMap[type];
    }

    /// <summary>
    /// /необходимо поведение для поиска работы, оно будет дефолтным      
    /// </summary>
    
    private void SetBehaviorByDefault()
    {
        this.SetBehaviorFishing();
    }

    /// <summary>
    ///  методы для установки поведения
    /// </summary>
    public void SetBehaviorFishing()
    {
        var behavior = this.GetBehavior<CatBehFishing>();
        this.SetBehavior(behavior);
    }

    public void SetBehaviorWorcking()
    {
        var behavior = this.GetBehavior<CatBehWorckOnSellersShop>();
        this.SetBehavior(behavior);
    }
    //==================================================================================

    private void SetTargetPositions()
    {
        if (Input.GetMouseButtonDown(0))
        {

            _targetPositions = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    private void SetAgentPosition()
    {

        _agent.SetDestination(new Vector3(_targetPositions.x, _targetPositions.y, transform.position.z));
    }
}
