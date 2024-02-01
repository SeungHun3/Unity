
/*
개발 과정중에는 종종 ***글로벌 변수***를 이용해야 하는 상황이 생긴다. 
특히 문자열 같은 경우 여러 클래스에 걸쳐 같은 문자열이 사용된다면 이를 글로벌 변수로 정의하여 공유하도록 하는것이 현명하다. 
단, 여기서 말하는 글로벌 변수들은 변하지 않는 변수들을 의미하므로 

**모두 const와 readonly로 정의한다** 

*/
public static class Globals
{
    public const int WorldSpaceUISortingOrder = 1;
	public const int CharacterStartSortingOrder = 10;

	public static class LayerName
	{
		public static readonly string Default = "Default";
		public static readonly string UI = "UI";
		public static readonly string Card = "Card";
		public static readonly string Obstacle = "Obstacle";
	}
}
