﻿using EleCho.GoCqHttpSdk.Message.DataModel;
using EleCho.GoCqHttpSdk.Utils;
using System;

namespace EleCho.GoCqHttpSdk.Message
{
    public record class CqTextMsg : CqMsg
    {
        public override string MsgType => Consts.MsgType.Text;

        public string Text { get; set; } = string.Empty;

        internal CqTextMsg()
        { }

        public CqTextMsg(string text)
        {
            Text = text;
        }

        internal override CqMsgDataModel? GetDataModel() => new CqTextMsgDataModel(Text);

        internal override void ReadDataModel(CqMsgDataModel? model)
        {
            var m = model as CqTextMsgDataModel;
            if (m == null)
                throw new ArgumentException();

            Text = m.text;
        }

        public static implicit operator CqTextMsg(string text) => new CqTextMsg(text);
    }
}