﻿using EleCho.GoCqHttpSdk;

using EleCho.GoCqHttpSdk.Post.Model;

namespace EleCho.GoCqHttpSdk.Post
{
    /// <summary>
    /// 群消息
    /// </summary>
    public class CqGroupMessagePostContext : CqMessagePostContext
    {
        public override CqMessageType MessageType => CqMessageType.Group;

        public long GroupId { get; set; }
        public CqAnonymousInfomation? Anonymous { get; set; }
        public CqGroupMessageSender Sender { get; set; } = new CqGroupMessageSender();
        
        internal CqGroupMessagePostContext() { }

        public CqGroupMessagePostQuickOperation QuickOperation { get; }
            = new CqGroupMessagePostQuickOperation();

        internal override object? QuickOperationModel => QuickOperation.GetModel();
        internal override void ReadModel(CqPostModel model)
        {
            base.ReadModel(model);

            if (model is not CqGroupMessagePostModel msgModel)
                return;

            GroupId = msgModel.group_id;
            Anonymous = msgModel.anonymous == null ? null : new CqAnonymousInfomation(msgModel.anonymous);
            Sender = new CqGroupMessageSender(msgModel.sender);
        }
    }
}