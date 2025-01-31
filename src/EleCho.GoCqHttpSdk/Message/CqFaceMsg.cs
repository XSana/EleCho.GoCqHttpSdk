﻿using EleCho.GoCqHttpSdk.Message.DataModel;
using EleCho.GoCqHttpSdk.Utils;
using System;

namespace EleCho.GoCqHttpSdk.Message
{
    /// <summary>
    /// QQ 表情
    /// </summary>
    public record class CqFaceMsg : CqMsg
    {
        public override string MsgType => Consts.MsgType.Face;

        /// <summary>
        /// QQ 表情 ID
        /// </summary>
        public int Id { get; set; }

        internal CqFaceMsg()
        { }

        public CqFaceMsg(int id) => Id = id;

        internal override CqMsgDataModel? GetDataModel() => new CqFaceMsgDataModel(Id);

        internal override void ReadDataModel(CqMsgDataModel? model)
        {
            var m = model as CqFaceMsgDataModel;
            if (m == null)
                throw new ArgumentException();

            Id = m.id;
        }

        public static string GetFaceNameFromId(int faceId)
        {
            return faceId switch
            {
                0 => "惊讶",
                1 => "撇嘴",
                2 => "色",
                3 => "发呆",
                4 => "得意",
                5 => "流泪",
                6 => "害羞",
                7 => "闭嘴",
                8 => "睡",
                9 => "大哭",
                10 => "尴尬",
                11 => "发怒",
                12 => "调皮",
                13 => "呲牙",
                14 => "微笑",
                15 => "难过",
                16 => "酷",
                18 => "抓狂",
                19 => "吐",
                20 => "偷笑",
                21 => "愉快",
                22 => "白眼",
                23 => "傲慢",
                24 => "饥饿",
                25 => "困",
                26 => "惊恐",
                27 => "流汗",
                28 => "憨笑",
                29 => "大兵",
                30 => "奋斗",
                31 => "咒骂",
                32 => "疑问",
                33 => "嘘",
                34 => "晕",
                35 => "折磨",
                36 => "衰",
                37 => "骷髅",
                38 => "敲打",
                39 => "再见",
                41 => "发抖",
                42 => "爱情",
                43 => "跳跳",
                46 => "猪头",
                49 => "拥抱",
                53 => "蛋糕",
                54 => "闪电",
                55 => "炸弹",
                56 => "刀",
                57 => "足球",
                59 => "便便",
                60 => "咖啡",
                61 => "饭",
                63 => "玫瑰",
                64 => "凋谢",
                66 => "爱心",
                67 => "心碎",
                69 => "礼物",
                74 => "太阳",
                75 => "月亮",
                76 => "强",
                77 => "弱",
                78 => "握手",
                79 => "胜利",

                85 => "飞吻",
                86 => "怄火",
                89 => "西瓜",
                96 => "冷汗",
                97 => "擦汗",
                98 => "抠鼻",
                99 => "鼓掌",
                100 => "糗大了",
                101 => "坏笑",
                102 => "左哼哼",
                103 => "右哼哼",
                104 => "哈欠",
                105 => "鄙视",
                106 => "委屈",
                107 => "快哭了",
                108 => "阴险",
                109 => "亲亲",
                110 => "吓",
                111 => "可怜",
                112 => "菜刀",
                113 => "啤酒",
                114 => "篮球",
                115 => "乒乓",
                116 => "示爱",
                117 => "瓢虫",
                118 => "抱拳",
                119 => "勾引",
                120 => "拳头",
                121 => "差劲",
                122 => "爱你",
                123 => "不",
                124 => "OK",
                125 => "转圈",
                126 => "磕头",
                127 => "回头",
                128 => "跳绳",
                129 => "挥手",
                130 => "激动",
                131 => "街舞",
                132 => "献吻",
                133 => "左太极",
                134 => "右太极",
                136 => "双喜",
                137 => "鞭炮",
                138 => "灯笼",
                139 => "发财",
                140 => "K歌",
                141 => "购物",
                142 => "邮件",
                143 => "帅",
                144 => "喝彩",
                145 => "祈祷",
                146 => "爆筋",
                147 => "棒棒糖",
                148 => "喝奶",
                149 => "下面",
                150 => "香蕉",
                151 => "飞机",
                152 => "开车",
                153 => "高铁左车头",
                154 => "车厢",
                155 => "高铁右车头",
                156 => "多云",
                157 => "下雨",
                158 => "钞票",
                159 => "熊猫",
                160 => "灯泡",
                161 => "风车",
                162 => "闹钟",
                163 => "打伞",
                164 => "彩球",
                165 => "钻戒",
                166 => "沙发",
                167 => "纸巾",
                168 => "药",
                169 => "手枪",
                170 => "青蛙",
                171 => "茶",
                172 => "眨眼睛",
                173 => "泪奔",
                174 => "无奈",
                175 => "卖萌",
                176 => "小纠结",
                177 => "喷血",
                178 => "斜眼笑",
                179 => "doge",
                180 => "惊喜",
                181 => "骚扰",
                182 => "笑哭",
                183 => "我最美",
                184 => "河蟹",
                185 => "羊驼",
                186 => "栗子",
                187 => "幽灵",
                188 => "蛋",
                189 => "跳舞机",
                190 => "菊花",
                191 => "香皂",
                192 => "红包",
                193 => "大笑",
                194 => "不开心",
                197 => "冷漠",
                198 => "呃",
                199 => "好棒",
                200 => "拜托",
                201 => "点赞",
                202 => "无聊",
                203 => "托脸",
                204 => "吃",
                205 => "送花",
                206 => "害怕",
                207 => "花痴",
                208 => "小样",
                210 => "飙泪",
                211 => "我不看",
                212 => "托腮",
                214 => "啵啵",
                215 => "糊脸",
                216 => "拍头",
                217 => "扯一扯",
                218 => "舔一舔",
                219 => "蹭一蹭",
                220 => "拽炸天",
                221 => "顶呱呱",

                _ => string.Empty
            };
        }

        public static int GetFaceIdFromName(string name)
        {
            return name switch
            {
                "惊讶" => 0,
                "撇嘴" => 1,
                "色" => 2,
                "发呆" => 3,
                "得意" => 4,
                "流泪" => 5,
                "害羞" => 6,
                "闭嘴" => 7,
                "睡" => 8,
                "大哭" => 9,
                "尴尬" => 10,
                "发怒" => 11,
                "调皮" => 12,
                "呲牙" => 13,
                "微笑" => 14,
                "难过" => 15,
                "酷" => 16,

                "抓狂" => 18,
                "吐" => 19,
                "偷笑" => 20,
                "愉快" => 21,
                "白眼" => 22,
                "傲慢" => 23,
                "饥饿" => 24,
                "困" => 25,
                "惊恐" => 26,
                "流汗" => 27,
                "憨笑" => 28,
                "大兵" => 29,
                "奋斗" => 30,
                "咒骂" => 31,
                "疑问" => 32,
                "嘘" => 33,
                "晕" => 34,
                "折磨" => 35,
                "衰" => 36,
                "骷髅" => 37,
                "敲打" => 38,
                "再见" => 39,

                "发抖" => 41,
                "爱情" => 42,
                "跳跳" => 43,
                "猪头" => 46,
                "拥抱" => 49,
                "蛋糕" => 53,
                "闪电" => 54,
                "炸弹" => 55,
                "刀" => 56,
                "足球" => 57,

                "便便" => 59,
                "咖啡" => 60,
                "饭" => 61,

                "玫瑰" => 63,
                "凋谢" => 64,
                "爱心" => 66,
                "心碎" => 67,
                "礼物" => 69,
                "太阳" => 74,
                "月亮" => 75,
                "强" => 76,
                "弱" => 77,
                "握手" => 78,
                "胜利" => 79,

                //"抱拳" => 80,
                //"勾引" => 81,
                //"拳头" => 82,
                //"差劲" => 83,
                //"爱你" => 84,

                "飞吻" => 85,
                "怄火" => 86,
                "西瓜" => 89,

                "冷汗" => 96,
                "擦汗" => 97,
                "抠鼻" => 98,
                "鼓掌" => 99,
                "糗大了" => 100,
                "坏笑" => 101,
                "左哼哼" => 102,
                "右哼哼" => 103,
                "哈欠" => 104,
                "鄙视" => 105,
                "委屈" => 106,
                "快哭了" => 107,
                "阴险" => 108,
                "亲亲" => 109,
                "吓" => 110,
                "可怜" => 111,
                "菜刀" => 112,
                "啤酒" => 113,
                "篮球" => 114,
                "乒乓" => 115,
                "示爱" => 116,
                "瓢虫" => 117,
                "抱拳" => 118,
                "勾引" => 119,
                "拳头" => 120,
                "差劲" => 121,
                "爱你" => 122,
                "不" => 123,
                "OK" => 124,
                "转圈" => 125,
                "磕头" => 126,
                "回头" => 127,
                "跳绳" => 128,
                "挥手" => 129,
                "激动" => 130,
                "街舞" => 131,
                "献吻" => 132,
                "左太极" => 133,
                "右太极" => 134,
                "双喜" => 136,
                "鞭炮" => 137,
                "灯笼" => 138,
                "发财" => 139,
                "K歌" => 140,
                "购物" => 141,
                "邮件" => 142,
                "帅" => 143,
                "喝彩" => 144,
                "祈祷" => 145,
                "爆筋" => 146,
                "棒棒糖" => 147,
                "喝奶" => 148,
                "下面" => 149,
                "香蕉" => 150,
                "飞机" => 151,
                "开车" => 152,
                "高铁左车头" => 153,
                "车厢" => 154,
                "高铁右车头" => 155,
                "多云" => 156,
                "下雨" => 157,
                "钞票" => 158,
                "熊猫" => 159,
                "灯泡" => 160,
                "风车" => 161,
                "闹钟" => 162,
                "打伞" => 163,
                "彩球" => 164,
                "钻戒" => 165,
                "沙发" => 166,
                "纸巾" => 167,
                "药" => 168,
                "手枪" => 169,
                "青蛙" => 170,

                "茶" => 171,
                "眨眼睛" => 172,
                "泪奔" => 173,
                "无奈" => 174,
                "卖萌" => 175,
                "小纠结" => 176,
                "喷血" => 177,
                "斜眼笑" => 178,
                "doge" => 179,
                "惊喜" => 180,
                "骚扰" => 181,
                "笑哭" => 182,
                "我最美" => 183,

                "河蟹" => 184,
                "羊驼" => 185,
                "栗子" => 186,
                "幽灵" => 187,
                "蛋" => 188,

                "跳舞机" => 189,

                "菊花" => 190,
                "香皂" => 191,
                "红包" => 192,
                "大笑" => 193,
                "不开心" => 194,
                "冷漠" => 197,
                "呃" => 198,
                "好棒" => 199,

                "拜托" => 200,
                "点赞" => 201,
                "无聊" => 202,
                "托脸" => 203,
                "吃" => 204,

                "送花" => 205,
                "害怕" => 206,
                "花痴" => 207,

                "小样" => 208,
                "飙泪" => 210,
                "我不看" => 211,
                "托腮" => 212,

                "啵啵" => 214,
                "糊脸" => 215,
                "拍头" => 216,
                "扯一扯" => 217,
                "舔一舔" => 218,
                "蹭一蹭" => 219,
                "拽炸天" => 220,
                "顶呱呱" => 221,

                _ => -1
            };
        }


        private static string[] allSupportedFaceNames = new string[]
        {
            "惊讶","撇嘴","色","发呆","得意","流泪","害羞",
            "闭嘴","睡","大哭","尴尬","发怒","调皮","呲牙",
            "微笑","难过","酷","抓狂","吐","偷笑","愉快",
            "白眼","傲慢","饥饿","困","惊恐","流汗","憨笑",
            "大兵","奋斗","咒骂","疑问","嘘","晕","折磨",
            "衰","骷髅","敲打","再见","发抖","爱情","跳跳",
            "猪头","拥抱","蛋糕","闪电","炸弹","刀","足球",
            "便便","咖啡","饭","玫瑰","凋谢","爱心","心碎",
            "礼物","太阳","月亮","强","弱","握手","胜利",
            "飞吻","怄火","西瓜","冷汗","擦汗","抠鼻","鼓掌",
            "糗大了","坏笑","左哼哼","右哼哼","哈欠","鄙视","委屈",
            "快哭了","阴险","亲亲","吓","可怜","菜刀","啤酒",
            "篮球","乒乓","示爱","瓢虫","抱拳","勾引","拳头",
            "差劲","爱你","不","OK","转圈","磕头","回头",
            "跳绳","挥手","激动","街舞","献吻","左太极","右太极",
            "双喜","鞭炮","灯笼","发财","K歌","购物","邮件",
            "帅","喝彩","祈祷","爆筋","棒棒糖","喝奶",
            "下面","香蕉","飞机","开车","高铁左车头","车厢","高铁右车头",
            "多云","下雨","钞票","熊猫","灯泡","风车","闹钟",
            "打伞","彩球","钻戒","沙发","纸巾","药","手枪",
            "青蛙","茶","眨眼睛","泪奔","无奈","卖萌","小纠结",
            "喷血","斜眼笑","doge","惊喜","骚扰","笑哭","我最美",
            "河蟹","羊驼","栗子","幽灵","蛋","跳舞机","菊花",
            "香皂","红包","大笑","不开心","冷漠","呃","好棒",
            "拜托","点赞","无聊","托脸","吃","送花","害怕",
            "花痴","小样","飙泪","我不看","托腮","啵啵","糊脸",
            "拍头","扯一扯","舔一舔","蹭一蹭","拽炸天","顶呱呱",

        };
        public static string[] GetAllSupportedFaceNames()
        {
            return allSupportedFaceNames;
        }

        public static CqFaceMsg? FromName(string name)
        {
            int fadeId = GetFaceIdFromName(name);
            if (fadeId == -1)
                return null;
            return new CqFaceMsg(fadeId);
        }
    }
}