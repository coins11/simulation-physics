import Data.Bits
import qualified Data.Map as M

step :: M.Map (Bool, Bool, Bool) Bool -> [Bool] -> [Bool]
step _ [] = []
step rule l@(x : xs) = step' (foldl (const id) x xs) x l where
    cell k = maybe False id $ M.lookup k rule
    step' h t [] = []
    step' h t [x] = [cell (h, x, t)]
    step' h t (x : xs@(x' : _)) = cell (h, x, x') : step' x t xs

runAuto :: Int -> [Bool] -> [[Bool]]
runAuto n = iterate $ step $ M.fromList
    [((x, y, z), odd (shiftR n (4*xn + 2*yn + zn))) |
        (x, xn) <- pl, (y, yn) <- pl, (z, zn) <- pl]
    where pl = [(False, 0), (True, 1)]
