// import path, { dirname } from 'node:path';
// import { fileURLToPath } from 'node:url';

// const __dirname = dirname(fileURLToPath(import.meta.url));
// const projectName = path.basename(path.join(__dirname, '..'));

/**
 * puer 脚手架配置
 */
const config = {
    // 配置项目 TS 文件目录
    tsProjectSrcDir: "./src",
    // 配置项目 TS 编译为 JS 所输出的目录（建议不动）
    tsOutputDir: "../Assets/StreamingAssets/data",
}
export default config;