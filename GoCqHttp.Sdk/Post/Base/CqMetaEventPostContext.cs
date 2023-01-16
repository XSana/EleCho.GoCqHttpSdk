﻿

namespace EleCho.GoCqHttpSdk.Post
{
    public abstract class CqMetaEventPostContext : CqPostContext
    {
        public override CqPostType EventType => CqPostType.MetaEvent;
        public abstract CqMetaEventType MetaEventType { get; }
    }
}