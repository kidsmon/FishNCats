using UnityEngine;
using System.Collections;

public class VerticalFishMain : FishMain
{
    // === 외부 파라미터(Inspector 표시) ===================
    public int aiIfMOVE        = 35;
    public int aiIfTURN        = 35;

    // === 외부 파라미터 ==================================

    // === 코드（AI사고 루틴 처리） ========================  
    public override void UpdateAI()
    {
        //Debug.Log(string.Format(">>> aists {0}", aiState));
        switch (aiState)
        {
            case FISHAISTS.ACTIONSELECT: // 사고 루틴 기점
                                         // 액션
                int n = SelectRandomAIState();
                if (n < aiIfMOVE)
                { // 시간 지정, 속도 지정
                    RandomSpeedXY = new Vector2(Random.Range(0.0f, 2.0f), Random.Range(-1.0f, 1.0f));
                    SetAIState(FISHAISTS.MOVE, Random.Range(2.0f, 5.0f), RandomSpeedXY);
                }
                else if (n < aiIfMOVE + aiIfTURN)
                {

                    fishCtrl.SpeedTurn();
                }
                else
                {
                    SetAIState(FISHAISTS.WAIT, 1.0f + Random.Range(0.0f, 1.5f));
                }
                fishCtrl.ActionMove(Vector2.zero);
                break;
            case FISHAISTS.MOVE:
                fishCtrl.ActionMove(RandomSpeedXY);
                break;
            case FISHAISTS.TRUN:
                break;
            case FISHAISTS.WAIT:
                fishCtrl.ActionMove(Vector2.zero);
                break;
            case FISHAISTS.EATFOOD:
                eating = true;
                if (fishCtrl.TargetFood[0])
                {
                    fishCtrl.ActionMoveToFood(0);
                }
                else if (!fishCtrl.TargetFood[0] && fishCtrl.TargetFood[1])
                {
                    fishCtrl.ActionMoveToFood(1);
                }
                else if (!fishCtrl.TargetFood[0] && !fishCtrl.TargetFood[1])
                {
                    eating = false;
                    SetAIState(FISHAISTS.CHANGEANGLE, 0.2f, Random.Range(-30.0f, +30.0f));
                }
                break;
        }
    }
}