/*

ExcelData => binary 파일, cs 파일 => 생성된 cs파일을 사용해 binary 데이터 read

code 예제 : 

DataTableLoader _dataTableLoader = null;
	void MyLog()
{
		// 객체생성, 경로넣어 생성자 호출
        _dataTableLoader = new DataTableLoader(Application.dataPath + "/DataTable"); 
		// 원하는 데이터테이블 형변환
		SlotDataTable binaryFileName = _dataTableLoader.GetDataTable(SlotDataTable_List.NAME,1) as SlotDataTable; 

        var Key = binaryFileName.Icon_Idle;
        Debug.Log(binaryFileName); => "name + Data"
		Debug.Log(Key); => "property"
}
 */