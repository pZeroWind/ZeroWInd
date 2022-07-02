const min = 1000 * 60
const hours = min * 60
const day = hours * 24

//清除标签
export function ClearHtml(html: string) {
	return html.replace(/<[^>]+>/g, "")
}

//时间转换
export function GetDate(obj: number) {
	let now = new Date().getTime()
	let date = (now - obj)

	if (date < min) {
		return Math.floor(toS(date)) + "秒前"
	} else if (date < hours) {
		return Math.floor(toMin(date)) + "分钟前"
	} else if (date < day) {
		return Math.floor(toHour(date)) + "小时前"
	} else if (date < (day * 7)) {
		return Math.floor(toDay(date)) + "天前"
	}
	let time = new Date(obj)
	return checkTime(time.getFullYear()) + "-" + checkTime((time.getMonth() + 1)) + "-" + checkTime(time.getDate())
}

function toS(date: number) {
	return date / 1000
}

function toMin(date: number) {
	return toS(date) / 60
}

function toHour(date: number) {
	return toMin(date) / 60
}

function toDay(date: number) {
	return toHour(date) / 24
}

function checkTime(obj: number) {
	if (obj < 10) {
		return "0" + obj
	}
	return obj
}
