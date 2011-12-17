
module Main where

import Control.Monad
import System.Random

points :: Int
points = 200

rand :: IO Int
rand = getStdRandom (randomR (-1000000, 1000000))

getPoint :: IO (Float, Float)
getPoint = do
    x <- rand
    y <- rand
    if x*x + y*y < 1000000000000
        then return (fromIntegral x / 1000000, fromIntegral y / 1000000)
        else getPoint

main :: IO ()
main = do
    r <- replicateM points getPoint
    mapM_ (\(x, y) -> putStrLn $ show x ++ " " ++ show y) r

