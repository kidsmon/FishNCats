using UnityEngine;
using System.Collections;


// 서버로부터 받은 패킷을 변환한 데이터를 저장하는 클래스들

/////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////



/// <PreHandlingMessage>
/// ////////////////////////////////////////////////////////////////////
class PreHandlingMessage
{
    public string Mtype { get; set; }
    public string DataJSON { get; set; }

}
/// 서버로 부터 Mtype: "", DataJSON: "{}" 이런식으로 정보를 받아온다.
/// DataJSON이 Message type에 따라 유동적으로 정보를 받아드릴수 있도록 한다.
/// DataJSON은 처음에 string으로 받기 때문에 Mtype에 따라 적당한 클래스에
/// 정보를 저장하도록 한다.

/// 
/// 
/// </PreHandlingMessage>
///////////////////////////////////////////////////////////////////////////
public class CJoinInfo
{
    public string UserID { get; set; }
    //인 게임내에서의 아이디
    //facebook id x
    public bool JoinState { get; set; }
    public string RetMessage { get; set; }
}
///////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////
public class CLoginInfo
{
    public string UserID { get; set; }
}
/// UserInfo에 대한 정보를 저장하기 위한 클래스
class CUserInfo
{
    public string UserID { get; set; }
    public CMainInfo MainInfo { get; set; }
    public CIllustBookList[] IllustBookList { get; set; }
}

class CMainInfo
{
//    public DateTime EndTime { get; set; }
    public string UserID { get; set; }
    public string UserPW { get; set; }
}
class CIllustBookList
{
    public string FishID { get; set; }
    public bool AcquireState { get; set; }
}

////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////// 
/// 
/// AquaListInfo에 대한 정보를 저장하기 위한 클래스
class CAquaList
{
    public string UserID { get; set; }
    public string AquaID { get; set; }
    public CAquaState AquaState { get; set; }
    public CThemaInfo ThemaInfo { get; set; }
    public CObjectInfo ObjectInfo { get; set; }
    public CFilterInfo FilterInfo { get; set; }
    public CMossInfo MossInfo { get; set; }
    public CFishInfo[] FishInfo { get; set; }


}

class CAquaState
{
    public int StatiefyGame { get; set; }
 //   public DateTime DiseaseTime { get; set; }
    public bool StrveState { get; set; }
    public bool CleanState { get; set; }
}
class CThemaInfo
{
    public string ThemaID { get; set; }
    public string ThemaName { get; set; }
}
class CObjectInfo
{
    public string ObjectID { get; set; }
    public string ObjectName { get; set; }
    public int ObjectSetNum { get; set; }
}
class CFilterInfo
{
    public string FilterID { get; set; }
    public string FilterName { get; set; }
}
class CMossInfo
{
    public int MossCount { get; set; }
 //   public DateTime LastCleanTime { get; set; }
}
class CFishInfo
{
    public string FishID { get; set; }
    public string FishName { get; set; }
    public string FishAge { get; set; }
    public bool DiseaseState { get; set; }
    public bool DieState { get; set; }
    public int FishPrice { get; set; }
    public int FishGrade { get; set; }
    public bool EvolutionState { get; set; }
    public int MateCount { get; set; }
}
///////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////

class CLotteryTab
{
    public string LotteryID { get; set; }
    public string LotteryName { get; set; }
    public int LotteryGrade { get; set; }
    public int LLotteryProbability { get; set; }
    public int MLotteryProbability { get; set; }
    public int ULotteryProbability { get; set; }
    public int LotteryPrice { get; set; }
}

///////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////

class CThemaTab
{
    public string ThemaID { get; set; }
    public string ThemaName { get; set; }
    public int ThemaPrice { get; set; }
}

//////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////

class CObjectTab
{
    public string ObjectID { get; set; }
    public string ObjectName { get; set; }
    public int ObjeectPrice { get; set; }
}

//////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////

class CEggList
{
    public string EggID { get; set; }
    public string EggName { get; set; }
    public int LBirthProbability { get; set; }
    public int MBirthProbability { get; set; }
    public int UBirthProbability { get; set; }
    public int EggGrade { get; set; }
    public int BirthTime { get; set; }
}

///////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////

class CFoodList
{
    public string FoodID { get; set; }
    public string FoodName { get; set; }
    public int FoodGrade { get; set; }
    public int FoodPrice { get; set; }
}

///////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////

class CDrugList
{
    public string DrugID { get; set; }
    public string DrugName { get; set; }
    public int DrugPrice { get; set; }
}

///////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////

class CTonicList
{
    public string TonicID { get; set; }
    public string TonicName { get; set; }
    public int TonicPrice { get; set; }
}

///////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////

class CFilterList
{
    public string FilterID { get; set; }
    public string FilterName { get; set; }
    public int FilterGrade { get; set; }
    public int FilterPrice { get; set; }
}

///////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////

class CIncubatorList
{
    public string FilterID { get; set; }
    public int EggCount { get; set; }
    public CMyEggList[] MyEggList { get; set; }
}

class CMyEggList
{
    public string EggID { get; set; }
    public string EggName { get; set; }
    public int EggGrade { get; set; }
//    public DateTime BirthStartTime { get; set; }
}

///////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////