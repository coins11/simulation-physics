import Control.Applicative
import Control.Monad
import Control.Arrow
import System.Random

rand :: IO Float
rand = (/ 1000000) . fromIntegral <$> getStdRandom (randomR (0, 4000000 :: Int))

main :: IO ()
main = do
    ps <- replicateM 200 (liftM2 (,) rand rand)
    mapM_ (putStrLn . unwords . map show) $
        map (\n -> map (\m -> length $ filter ((n, m) ==)
            (map (round *** round) ps)) [0..4]) [0..4]

