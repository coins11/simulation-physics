
module Main where

import Control.Monad
import System.Random

points :: Int
points = 100000

rand :: IO Int
rand = getStdRandom (randomR (-1000000, 1000000))

main :: IO ()
main = do
    r <- replicateM points (liftM2 (,) rand rand)
    let len = length $ filter (\(x, y) -> (x*x + y*y) < 1000000000000) r
    let r = fromIntegral (4 * len) / fromIntegral points
    print $ r
    print $ (pi - r) / pi

