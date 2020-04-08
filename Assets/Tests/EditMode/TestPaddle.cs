using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class TestPaddle
    {

        [Test]
        public void TestGetUpdatedPaddlePosition_ShouldReturnNonClampedValue()
        {
            // creates a new paddle game object
            Paddle paddle = new GameObject().AddComponent<Paddle>();
            paddle.minRelativePosX = 1;
            paddle.maxRelativePosX = 15;
            paddle.fixedRelativePosY = 0.64f;
            
            // method invocation
            Vector2 vector = paddle.GetUpdatedPaddlePosition(10f);
            Vector2 expectedVector = new Vector2(10f, 0.64f );

            Assert.AreEqual(expectedVector, vector);
        }
        
        [Test]
        public void TestGetUpdatedPaddlePosition_ShouldReturnClampedMaxValue()
        {
            // creates a new paddle game object
            Paddle paddle = new GameObject().AddComponent<Paddle>();
            paddle.fixedRelativePosY = 0.64f;
            paddle.minRelativePosX = 1;
            paddle.maxRelativePosX = 15;
            
            // method invocation with clamped value
            Vector2 vector = paddle.GetUpdatedPaddlePosition(17); // above max
            Vector2 expectedVector = new Vector2(15f, 0.64f );

            Assert.AreEqual(expectedVector, vector);
        }
        
        [Test]
        public void TestGetUpdatedPaddlePosition_ShouldReturnClampedMinValue()
        {
            // creates a new paddle game object
            Paddle paddle = new GameObject().AddComponent<Paddle>();
            paddle.fixedRelativePosY = 0.64f;
            paddle.minRelativePosX = 1;
            paddle.maxRelativePosX = 15;
            
            // method invocation with clamped value
            Vector2 vector = paddle.GetUpdatedPaddlePosition(0); // below min
            Vector2 expectedVector = new Vector2(1f, 0.64f );

            Assert.AreEqual(expectedVector, vector);
        }


        
        [Test]
        public void TestConvertPixelToRelativePosition_ShouldConvertToFarthestX()
        {
            // creates a new paddle game object with its state
            int fakeScreenWidth = 32;
            Paddle paddle = new GameObject().AddComponent<Paddle>();
            paddle.screenWidthUnits = 16;
            
            // method invocation
            float relativeValue = paddle.ConvertPixelToRelativePosition(32, fakeScreenWidth);  // farthest X possible
            
            // assertions
            Assert.AreEqual(16f, relativeValue);
        }
        
        [Test]
        public void TestConvertPixelToRelativePosition_ShouldConvertToClosestX()
        {
            // creates a new paddle game object with its state
            int fakeScreenWidth = 32;
            Paddle paddle = new GameObject().AddComponent<Paddle>();
            paddle.screenWidthUnits = 16;
            
            // method invocation
            float relativeValue = paddle.ConvertPixelToRelativePosition(0, fakeScreenWidth);
            
            // assertions
            Assert.AreEqual(0, relativeValue);
        }
        
    }
}
