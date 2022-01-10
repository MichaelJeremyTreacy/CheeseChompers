using System.Collections;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerMovementTests
{
    private GameObject _floorGameObject;

    [OneTimeSetUp]
    public void FloorSetUp()
    {
        Debug.Log("Setting up Floor");

        _floorGameObject = GameObject.Instantiate(Resources.Load("FloorPrototype64x01x64")) as GameObject;

        Assert.IsNotNull(_floorGameObject);
    }

    [UnityTest]
    public IEnumerator FirstPerson_PositiveHorizontal_MoveRight()
    {
        Debug.Log("Running FirstPerson_PositiveHorizontal_MoveRight");

        GameObject rightPlayerGameObject = GameObject.Instantiate(Resources.Load("Player")) as GameObject;
        PlayerController rightPlayerController = rightPlayerGameObject.GetComponent<PlayerController>();
        rightPlayerController.PlayersInput = Substitute.For<IPlayerInput>();

        Assert.IsNotNull(rightPlayerController.PlayersInput);

        rightPlayerGameObject.transform.localPosition = Vector3.zero;
        rightPlayerController.PlayersInput.Horizontal.Returns(1f);

        yield return new WaitForSeconds(1f);

        Assert.IsTrue(rightPlayerGameObject.transform.position.x > 0f);
        Assert.AreEqual(0, rightPlayerGameObject.transform.position.z);
        Assert.AreEqual(0, rightPlayerGameObject.transform.position.y);
    }

    [UnityTest]
    public IEnumerator FirstPerson_NegativeHorizonal_MoveLeft()
    {
        Debug.Log("Running FirstPerson_NegativeHorizonal_MoveLeft");

        GameObject leftPlayerGameObject = GameObject.Instantiate(Resources.Load("Player")) as GameObject;
        PlayerController leftPlayerController = leftPlayerGameObject.GetComponent<PlayerController>();
        leftPlayerController.PlayersInput = Substitute.For<IPlayerInput>();

        Assert.IsNotNull(leftPlayerController.PlayersInput);

        leftPlayerGameObject.transform.localPosition = Vector3.zero;
        leftPlayerController.PlayersInput.Horizontal.Returns(-1f);

        yield return new WaitForSeconds(1f);

        Assert.IsTrue(leftPlayerGameObject.transform.position.x < 0f);
        Assert.AreEqual(0, leftPlayerGameObject.transform.position.z);
        Assert.AreEqual(0, leftPlayerGameObject.transform.position.y);
    }

    [UnityTest]
    public IEnumerator FirstPerson_PositiveVertical_MoveForward()
    {
        Debug.Log("Running FirstPerson_PositiveVertical_MoveForward");

        GameObject forwardPlayerGameObject = GameObject.Instantiate(Resources.Load("Player")) as GameObject;
        PlayerController forwardPlayerController = forwardPlayerGameObject.GetComponent<PlayerController>();
        forwardPlayerController.PlayersInput = Substitute.For<IPlayerInput>();

        Assert.IsNotNull(forwardPlayerController.PlayersInput);

        forwardPlayerGameObject.transform.localPosition = Vector3.zero;
        forwardPlayerController.PlayersInput.Vertical.Returns(1f);

        yield return new WaitForSeconds(1f);

        Assert.IsTrue(forwardPlayerGameObject.transform.position.z > 0f);
        Assert.AreEqual(0, forwardPlayerGameObject.transform.position.x);
        Assert.AreEqual(0, forwardPlayerGameObject.transform.position.y);
    }

    [UnityTest]
    public IEnumerator FirstPerson_NegativeVertical_MoveBackward()
    {
        Debug.Log("Running FirstPerson_NegativeVertical_MoveBackward");

        GameObject backwardPlayerGameObject = GameObject.Instantiate(Resources.Load("Player")) as GameObject;
        PlayerController backwardPlayerController = backwardPlayerGameObject.GetComponent<PlayerController>();
        backwardPlayerController.PlayersInput = Substitute.For<IPlayerInput>();

        Assert.IsNotNull(backwardPlayerController.PlayersInput);

        backwardPlayerGameObject.transform.localPosition = Vector3.zero;
        backwardPlayerController.PlayersInput.Vertical.Returns(-1f);

        yield return new WaitForSeconds(1f);

        Assert.IsTrue(backwardPlayerGameObject.transform.position.z < 0f);
        Assert.AreEqual(0, backwardPlayerGameObject.transform.position.x);
        Assert.AreEqual(0, backwardPlayerGameObject.transform.position.y);
    }
}
