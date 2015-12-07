using UnityEngine;
using System.Collections;

public class HorizontalFishMain : FishMain
{
    // === 외부 파라미터(Inspector 표시) ==============
    public int aiIfFORWARD     = 20;
    public int aiIfCHANGEANGLE = 20;
    public int aiIfSPIRALMOVE  = 20;
    public int aiIfTURN        = 20;

    // === 내부 파라미터 =============================

    // === 코드（AI사고 루틴 처리） ===================
    public override void UpdateAI()
    {
        //Debug.Log(string.Format(">>> aists {0}", aiState));
        switch (aiState)
        {
            case FISHAISTS.ACTIONSELECT: // 사고 루틴 기점
                                         // 액션
                int n = SelectRandomAIState();
                if (n < aiIfFORWARD)
                { // 시간 지정, 속도 지정
                    SetAIState(FISHAISTS.FORWARD, Random.Range(2.0f, 5.0f), Random.Range(0.5f, 4.0f));
                }
                else if (n < aiIfFORWARD + aiIfCHANGEANGLE)
                {
                    SetAIState(FISHAISTS.CHANGEANGLE, 0.2f, Random.Range(-30.0f, +30.0f));
                }
                else if (n < aiIfFORWARD + aiIfCHANGEANGLE + aiIfSPIRALMOVE)
                {// 시간 지정, 속도 지정, 각도 지정
                    SetAIState(FISHAISTS.SPIRALMOVE, SetRandomTime(),
                        Random.Range(0.5f, 5.0f), Random.Range(-30.0f, +30.0f));
                }
                else if (n < aiIfFORWARD + aiIfCHANGEANGLE + aiIfSPIRALMOVE + aiIfTURN)
                {
                    fishCtrl.SpeedTurn();
                }
                else
                {
                    SetAIState(FISHAISTS.WAIT, 1.0f + Random.Range(0.0f, 1.5f));
                }
                fishCtrl.ActionForward(0.0f);
                break;
            case FISHAISTS.FORWARD:
                fishCtrl.ActionForward(RandomSpeed);
                break;
            case FISHAISTS.CHANGEANGLE:
                fishCtrl.ActionChagneAngle(RandomAngle * 5 * Time.deltaTime);
                break;
            case FISHAISTS.SPIRALMOVE:
                fishCtrl.ActionForward(RandomSpeed / RandomTime);
                fishCtrl.ActionChagneAngle(RandomAngle / RandomTime * Time.deltaTime);
                break;
            case FISHAISTS.TRUN:
                //fishCtrl.ActionTurn();
                break;
            case FISHAISTS.WAIT:
                fishCtrl.ActionForward(0.0f);
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