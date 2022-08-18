import axios from "../plugins/axios";

const url = "/Tag"

export class Tag {
    Get(val: string, out: Function) {
        axios.get(url, {
            val
        }).then(res => {
            out(res)
        })
    }
}