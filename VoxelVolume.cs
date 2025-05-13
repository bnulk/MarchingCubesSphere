using OpenTK.Mathematics; // 用于 Vector3

namespace MarchingCubesSphere
{
    /// <summary>
    /// 表示一个三维密度场（标量场），用于 Marching Cubes。
    /// </summary>
    public class VoxelVolume
    {
        public readonly int SizeX, SizeY, SizeZ;     // 网格在 X/Y/Z 方向的格点数
        public readonly float[,,] Density;           // 三维标量值数组
        public readonly float VoxelSize;             // 每个体素的边长（物理空间大小）

        /// <summary>
        /// 构造体素体积，初始化密度值为球体的隐式函数。
        /// </summary>
        public VoxelVolume(int sizeX, int sizeY, int sizeZ, float voxelSize)
        {
            SizeX = sizeX;
            SizeY = sizeY;
            SizeZ = sizeZ;
            VoxelSize = voxelSize;

            Density = new float[SizeX, SizeY, SizeZ];

            GenerateSphereDensity();
        }

        /// <summary>
        /// 为球体生成标量密度值：f(x, y, z) = x² + y² + z² - R²
        /// </summary>
        private void GenerateSphereDensity()
        {
            // 计算网格中心点位置（单位为格点）
            Vector3 center = new Vector3(SizeX / 2f, SizeY / 2f, SizeZ / 2f);
            float radius = MathF.Min(SizeX, MathF.Min(SizeY, SizeZ)) * VoxelSize * 0.4f; // 设置半径为 40% 较短轴长度
            float radiusSquared = radius * radius;

            // 遍历所有格点，计算密度值
            for (int x = 0; x < SizeX; x++)
            {
                for (int y = 0; y < SizeY; y++)
                {
                    for (int z = 0; z < SizeZ; z++)
                    {
                        // 当前格点在物理空间中的实际位置
                        Vector3 pos = new Vector3(x, y, z) * VoxelSize;

                        // 到中心的距离向量
                        Vector3 offset = pos - center * VoxelSize;

                        // 使用隐函数：f(x, y, z) = x² + y² + z² - R²
                        Density[x, y, z] = offset.LengthSquared - radiusSquared;
                    }
                }
            }
        }

        /// <summary>
        /// 获取指定格点处的密度值，支持边界检查。
        /// </summary>
        public float GetDensity(int x, int y, int z)
        {
            if (x < 0 || x >= SizeX ||
                y < 0 || y >= SizeY ||
                z < 0 || z >= SizeZ)
                return float.MaxValue; // 越界返回一个大值

            return Density[x, y, z];
        }
    }
}