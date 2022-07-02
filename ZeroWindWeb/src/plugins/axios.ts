import axios from "axios"
import axiosConfig from "./axiosConfig"
import { ElNotification } from "element-plus"
//基础url
axios.defaults.baseURL = axiosConfig.baseURL
axios.defaults.headers.post["Content-Type"] = "application/json;charset=UTF-8";
//设置超时
axios.defaults.timeout = 10000;
//token拦截器
axios.interceptors.request.use(config => {
	const token = sessionStorage.getItem("token");
	if (token) {
		config.headers!.Authorization = token;
	}
	return config;
})

axios.interceptors.request.use(
	config => {
		return config;
	},
	error => {
		return Promise.reject(error);
	}
);

axios.interceptors.response.use(
	response => {
		if (response.status == 200) {
			if (response.data.code != 200) {
				ElNotification({
					title: '错误',
					message: response.data.msg,
					type: 'error',
					position: 'bottom-right',
					showClose: false,
					duration: 2500
				})
				if (response.data.code == 401) {
					sessionStorage.removeItem("token")
					window.location.href = "/sys"
				}
			}
			return Promise.resolve(response);
		} else {
			return Promise.reject(response);
		}
	},
	error => {
		ElNotification({
			title: '错误',
			message: error.message,
			type: 'error',
			position: 'bottom-right',
			showClose: false,
			duration: 2500
		})
	}
);
const Axios = {
	post(url: string, data: object, params: object) {
		return new Promise((resolve, reject) => {
			axios({
				method: 'post',
				url,
				data: data,
				params: params
			})
				.then(res => {
					resolve(res.data)
				})
				.catch(err => {
					reject(err)
				});
		})
	},

	get(url: string, params: object) {
		return new Promise((resolve, reject) => {
			axios({
				method: 'get',
				url,
				params: params,
			})
				.then(res => {
					resolve(res.data)
				})
				.catch(err => {
					reject(err)
				})
		})
	},

	put(url: string, data: object, params: object) {
		return new Promise((resolve, reject) => {
			axios({
				method: 'put',
				url,
				data: data,
				params: params
			})
				.then(res => {
					resolve(res.data)
				})
				.catch(err => {
					reject(err)
				})
		})
	},

	delete(url: string, params: object) {
		return new Promise((resolve, reject) => {
			axios({
				method: 'delete',
				url,
				params: params,
			})
				.then(res => {
					resolve(res.data)
				})
				.catch(err => {
					reject(err)
				})
		})
	},
};

export default Axios;