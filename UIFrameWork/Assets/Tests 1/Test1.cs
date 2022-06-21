using System;
using System.Collections;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.TestTools;
using Object = UnityEngine.Object;

public class Test1
{
    [UnityTest]
    public IEnumerator AsteroidsMoveDown()
    {

        var camera = new GameObject();
        camera.AddComponent<Camera>();
        camera.tag = "MainCamera";
        var isForced = true;
        var gameobject = new GameObject();
        gameobject.name = "ddddddddddddddddddddddddddd";
        //var rbc = camera.AddComponent<Rigidbody>();
        //var rb = gameobject.AddComponent<Rigidbody>();

        yield return new WaitForSeconds(0.1f);

        camera.FixedUpdateAsObservable()
            .Where(_ => isForced)
            .Subscribe(_ => {
                camera.transform.LookAt(gameobject.transform);
                camera.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y + 0.5f * Time.deltaTime, camera.transform.position.z);
            });

        gameobject.FixedUpdateAsObservable()
            .Where(_ => isForced)
            .Subscribe(_ =>
            {
                gameobject.transform.position = new Vector3(gameobject.transform.position.x, gameobject.transform.position.z + 0.5f * Time.deltaTime, gameobject.transform.position.z);
            });

        //gameobject.FixedUpdateAsObservable()
        //    .Where(_ => gameobject.CompareTag("WarpZone"))
        //    .Subscribe(_ => isForced = true);


        //gameobject.FixedUpdateAsObservable()
        //    .Where(_ => gameobject.CompareTag("WarpZone"))
        //    .Subscribe(_ => isForced = false);
        yield return new WaitForSeconds(30f);
        Observable.Timer(TimeSpan.FromSeconds(30)).Subscribe(_ =>
        {
            Object.DestroyImmediate(camera);
            Object.DestroyImmediate(gameobject);
        });
    }
}
