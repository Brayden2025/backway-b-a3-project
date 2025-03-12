using System;
using System.Numerics;

namespace MohawkGame2D
{
    public class Game
    {
        Vector2[] platformsPosition = {
            new Vector2(100, 200), new Vector2(200, 300), new Vector2(300, 150),
            new Vector2(400, 250), new Vector2(500, 350), new Vector2(600, 450),
            new Vector2(150, 400), new Vector2(250, 400), new Vector2(350, 44),
        };

        Vector2 playerPosition = new Vector2(100, 100);
        Vector2 playerVelocity = new Vector2(0, 0); 
        int playerSpeed = 7;

        Vector2 enemyPosition = new Vector2(400, 300);
        Vector2 enemyvelocity = new Vector2(0, 0);
        float enemySpeed = 3.0f;
        float Timeofday = 30;
        public void Setup()
        {
            Window.SetSize(800, 600);
            Window.SetTitle("Brayden Assignment 3");
        }

        public void Update()
        {
            Window.ClearBackground(Color.White);
            // when timer hits 0 timer restarts 
            Timeofday -= Time.DeltaTime;
            if (Timeofday <= 0)
            {
                Timeofday = 0;
             
            }
            //draw.circle(4, 4, 4);

            Text.Color = Color.Black;
            Text.Draw($"{Timeofday}", 300, 300);

            DrawPlatforms();
            PlayerMovement();
            UpdateEnemyPosition();
            DrawPlayer();
            DrawEnemy();
            UpdatePlayerPosition();
            CheckCollision();
            PreventPlayerFromGoingThroughPlatforms();
        }

        void DrawPlayer()
        {
            // Draw player
            Draw.FillColor = Color.Blue;
            Draw.Rectangle(playerPosition.X, playerPosition.Y, 50, 50); // Adjusted dimensions
        }

        void DrawEnemy()
        {
            // Draw enemy
            Draw.FillColor = Color.Red;
            Draw.Circle(enemyPosition.X, enemyPosition.Y, 15);
        }

        void DrawPlatforms()
        {
            Draw.FillColor = Color.Gray;
            foreach (var position in platformsPosition)
            {
                Draw.Rectangle(position.X, position.Y, 100, 10);
            }
        }

        void PlayerMovement()
        {
            if (Input.IsKeyboardKeyDown(KeyboardInput.Left))
            {
                playerVelocity.X = -playerSpeed; // Move player left
            }
            else if (Input.IsKeyboardKeyDown(KeyboardInput.Right))
            {
                playerVelocity.X = playerSpeed; // Move player right
            }
            else if (Input.IsKeyboardKeyDown(KeyboardInput.Up))
            {
                playerVelocity.Y = -playerSpeed; // Move player up
            }
            else if (Input.IsKeyboardKeyDown(KeyboardInput.Down))
            {
                playerVelocity.Y = playerSpeed; // Move player down
            }
            else
            {
                playerVelocity = Vector2.Zero; // Stop player movement if no key is pressed
            }
        }

        void UpdateEnemyPosition()
        {
            Vector2 direction = Vector2.Normalize(playerPosition - enemyPosition);
            enemyPosition += direction * enemySpeed;
        }

        void UpdatePlayerPosition()
        {
            playerPosition += playerVelocity; // Corrected to update playerPosition

            // Prevent player from moving off-screen 
            playerPosition.X = Math.Clamp(playerPosition.X, 0, 750);
            playerPosition.Y = Math.Clamp(playerPosition.Y, 0, 550);
        }

        void CheckCollision()
        {
            float distance = Vector2.Distance(playerPosition, enemyPosition);
            if (enemyPosition == playerPosition)
            {
                // End game logic
                Console.WriteLine("Game Over! The enemy has caught you.");
            }
        }

        void PreventPlayerFromGoingThroughPlatforms()
        {
            foreach (var platform in platformsPosition)
            {
                if (playerPosition.X + 50 > platform.X && playerPosition.X < platform.X + 100 &&
                    playerPosition.Y + 50 > platform.Y && playerPosition.Y < platform.Y + 10)
                {
                    // Collision detected, prevent player from going through the platform
                    if (playerVelocity.Y < 0) // Moving up
                    {
                        playerPosition.Y = platform.Y + 10;
                        playerVelocity.Y = 0;
                    }
                    else if (playerVelocity.Y > 0) // Moving down
                    {
                        playerPosition.Y = platform.Y - 50;
                        playerVelocity.Y = 0;
                    }
                    else if (playerVelocity.X < 0) // Moving left
                    {
                        playerPosition.X = platform.X + 100;
                        playerVelocity.X = 0;
                    }
                    else if (playerVelocity.X > 0) // Moving right
                    {
                        playerPosition.X = platform.X - 50;
                        playerVelocity.X = 0;

                    }
                    // collision detected,  prevent enemy from going through platforms 

                    if (enemyvelocity.Y < 0) // Moving up
                    {
                        enemyPosition.Y = platform.Y + 10;
                        enemyvelocity.Y = 0;
                    }
                    else if (enemyvelocity.Y > 0) // Moving down
                    {
                        enemyPosition.Y = platform.Y - 50;
                        enemyvelocity.Y = 0;
                    }
                    else if (enemyvelocity.X < 0) // Moving left
                    {
                        enemyPosition.X = platform.X + 100;
                        enemyvelocity.X = 0;
                    }
                    else if (enemyvelocity.X > 0) // Moving right
                    {
                        enemyPosition.X = platform.X - 50;
                        enemyvelocity.X = 0;

                    }
                    
                    void gameCheckCollision()
                    {
                        float enemyposition = Vector2.Distance(playerPosition, enemyPosition);
                        if (enemyPosition == playerPosition)
                        
                            // End game logic
                            Console.WriteLine("Game Over! The enemy has caught you.");
                        
                    }


                }
            }
        }
    }
}
