const axiosConfig = {
    //baseURL: "http://zerowind.top:5001" //正式
    baseURL: "http://127.0.0.1:5001/api" //测试
}

//返回内容模型
export interface ResultModel<T> {
    code: number,
    data: T,
    msg: string
}

//返回包含页面模型
export interface PageModel<T> {
    page: number,
    count: number,
    size: number,
    data: Array<T>
}

export default axiosConfig