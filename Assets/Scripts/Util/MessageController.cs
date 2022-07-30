using UnityEngine;

public abstract class MessageController<T>:MonoBehaviour where T: Message
{
    protected virtual void OnEnable()
    {
        MessageSystem.Instance.Register<T>(this.MessageHandler);
    }

    protected virtual void OnDisable()
    {
        if (!MessageSystem.Ative) return;
        MessageSystem.Instance.UnRegister<T>(this.MessageHandler);
    }

    protected abstract bool MessageHandler(Message message);
}
