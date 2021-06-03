import axios from "axios";

const request = axios.create({
    timeout: 150000
})

export default request;