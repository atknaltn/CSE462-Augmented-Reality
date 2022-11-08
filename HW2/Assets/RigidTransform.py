import UnityEngine
import numpy as np




data1 = np.genfromtxt('C:\\Users\\atkna\\HW2\\Assets\\Resources\\Coordinates1.txt', delimiter=' ',skip_header=1)
data2 = np.genfromtxt('C:\\Users\\atkna\\HW2\\Assets\\Resources\\Coordinates2.txt', delimiter=' ',skip_header=1)



def rigid_transform_3D(A, B):
    assert A.shape == B.shape

    num_rows, num_cols = A.shape
    if num_rows != 3:
        raise Exception(f"matrix A is not 3xN, it is {num_rows}x{num_cols}")

    num_rows, num_cols = B.shape
    if num_rows != 3:
        raise Exception(f"matrix B is not 3xN, it is {num_rows}x{num_cols}")


    centroid_A = np.mean(A, axis=1)
    centroid_B = np.mean(B, axis=1)


    centroid_A = centroid_A.reshape(-1, 1)
    centroid_B = centroid_B.reshape(-1, 1)


    Am = A - centroid_A
    Bm = B - centroid_B

    H = Am @ np.transpose(Bm)



    U, S, Vt = np.linalg.svd(H)
    R = Vt.T @ U.T

    if np.linalg.det(R) < 0:
        Vt[2,:] *= -1
        R = Vt.T @ U.T

    t = -R @ centroid_A + centroid_B

    return R, t

for i in range (100):
    randomRows1 = data1[np.random.choice(data1.shape[0], size=3, replace=False), :]
    randomRows2 = data2[np.random.choice(data2.shape[0], size=3, replace=False), :]
    A = randomRows1
    B = randomRows2

    ret_R, ret_t = rigid_transform_3D(A, B)

    UnityEngine.Debug.Log("R: ")
    UnityEngine.Debug.Log(ret_R.__str__())

    UnityEngine.Debug.Log("T: ")
    UnityEngine.Debug.Log(ret_t.__str__())




