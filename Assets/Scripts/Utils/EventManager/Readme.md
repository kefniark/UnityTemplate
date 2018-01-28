## Description
Small tool used to implement Publish-Subscribe pattern (https://en.wikipedia.org/wiki/Publish%E2%80%93subscribe_pattern)

Mostly based on https://github.com/kyubuns/Kuchen

## Documentation

### Basic

```C#
public class SimpleSample : MonoBehaviour
{
    void Start()
    {
        this.Subscribe("SampleTopic", () => {
            Debug.Log("baum");
        });
    }
}
```
```C#
public class SimpleButton : MonoBehaviour
{
    void OnClick()
    {
        this.Publish("SampleTopic");
    }
}
```

### With Arguments

```C#
public class WithArgs : MonoBehaviour
{
    void Start()
    {
        this.Subscribe("SampleTopic", (string message, int number) => {
            Debug.LogFormat("{0}: {1}", message, number);
        });

        this.Publish("SampleTopic", "test message", 611);
    }
}
```

### Wait Coroutine

```C#
public class CoroutineSample : MonoBehaviour
{
    public IEnumerator Start()
    {
        Debug.Log("SampleTopicが送信されるまで待つよ。");
        yield return this.WaitForMessage("SampleTopic");
        Debug.Log("SampleTopicが呼ばれたよ！");
    }
}
```

### Wildcard

```C#
public class Wildcard : MonoBehaviour
{
    void Start()
    {
        this.SubscribeWithTopic("Topic.*", (topic) => { Debug.Log(topic); });

        this.Publish("Topic.Hoge");
        this.Publish("Topic.Fuga");
    }
}
```

### Subscribe multiple topic

```C#
public class Multiple : MonoBehaviour
{
    void Start()
    {
        this.SubscribeWithTopic(new string[]{"Topic.Hoge", "Topic.Fuga"}, (topic) => { Debug.Log(topic); });

        this.Publish("Topic.Hoge");
        this.Publish("Topic.Fuga");
    }
}
```

### Subscribe Once

```C#
public class SubscribeOnce : MonoBehaviour
{
    void Start()
    {
        this.Subscribe("SampleTopic", () => { Debug.Log("!"); }).Once();

        this.Publish("SampleTopic");
        this.Publish("SampleTopic"); // 2回目は呼び出されない
    }
}
```

### SubscribeAndStartCoroutine

```C#
public class SubscribeAndStartCoroutine : MonoBehaviour
{
    void Start()
    {
        this.SubscribeAndStartCoroutine("SampleTopic", Coroutine);
        this.Publish("SampleTopic");
    }

    IEnumerator Coroutine()
    {
        yield return null;
    }
}
```

### Mute

```C#
public class SubscribeWithCoroutine : MonoBehaviour
{
    void Start()
    {
        this.SubscribeWithCoroutine("SampleTopic", () => { Debug.Log("!"); });

        this.Publish("SampleTopic");

        this.Mute("SampleTopic");
        this.Publish("SampleTopic"); // Muteしてる間は呼ばれない
        this.Unmute("SampleTopic");

        this.Publish("SampleTopic");
    }
}
```

### Without GameObject

```C#
public class NonGameObject
{
	void SubscribeTest()
	{
		using(var subscriber = new Subscriber())
		{
			subscriber.Subscribe("SampleTopic", () => { /* hoge */ });
			Publisher.Publish("SampleTopic");
		}
	}
}
```