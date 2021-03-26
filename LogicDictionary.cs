//---------------------------------------------------
//##    2021/02/04 作成
//##    細かいロジック系のプログラムをまとめたスクリプト
//##    ちょっとしたループ系などの処理を格納
//##
//##
//##
//---------------------------------------------------

namespace iwata
{
    public class Logics : MonoBehaviour
    {
        private void Start()
        {
            var list = new List<int>();
            // 値を入れる系ループ
            for (var i = 0; i < 10; i++)
            {
                list.Add(i);
            }
        }
        


        // ミッションなどのリスト取得ロジック（グループナンバー対応、クリア済み仕分け
        private List<Sample.Trophy_Item?> GetList()
        {
            var list = new List<Sample.Trophy_Item?>();
            var complist = new List<Sample.Trophy_Item?>();
            var trophyData = MasterReader.Instance.m_TrophyData;

            for (var i = 0; i < trophyData.DataLength; i++)
            {
                var groupNo = trophyData.Data(i).Value.GroupNo;
                var groupCount = 0;
                while (groupNo == trophyData.Data(groupCount + i).Value.GroupNo)
                {
                    groupCount++;
                    if (groupCount + i == trophyData.DataLength)
                    {
                        break;
                    }
                }

                // グループの中から未クリア取得
                for (var j = 0; j < groupCount; j++)
                {
                    var data = trophyData.Data(j + i);
                    // コンプ済みチェック
                    if (j == groupCount - 1 && SystemParam.PlayerGameData.Trophy[j + i] == true)
                    {
                        complist.Add(data);
                    }
                    else
                    {
                        if (SystemParam.PlayerGameData.Trophy[j + i] == true)
                        {
                            continue;
                        }
                        else
                        {
                            list.Add(data);
                            break;
                        }
                    }
                }
                i += (groupCount - 1);
            }
            list.AddRange(complist);
            return list;
        }

    }
}