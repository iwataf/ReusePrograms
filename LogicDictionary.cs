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
        // 重みを合算して抽選（重み整数以上）
        // 小数点以下の場合は整数に揃える必要あり
        private void LotteryPresent()
        {
            var present = new Sample.Present_Item();
            Sample.Present data = MasterReader.Instance.m_PresentData;
            var list = new List<Sample.Present_Item?>();
            for (var i = 0; i < data.DataLength; i++)
            {
                if (data.Data(i).Value.GroupNo == GroupNo)
                {
                    list.Add(data.Data(i));
                }
            }
            var totalWeight = 0;
            for (var i = 0; i < list.Count; i++)
            {
                totalWeight += list[i].Value.PresentRatio;
            }

            var value = UnityEngine.Random.Range(1, totalWeight + 1);
            for (var i = 0; i < list.Count; ++i)
            {
                if (list[i].Value.PresentRatio >= value)
                {
                    present = list[i].Value;
                    Debug.Log(present.ID);
                    break;
                }
                value -= list[i].Value.PresentRatio;
            }
        }

    }
}