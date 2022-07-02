import axios from "../plugins/axios";

const url = "/Tags"

export class Tag {
    Get(val: string, out: Function) {
        axios.get(url, {
            val
        }).then(res => {
            out(res)
        })
    }
}