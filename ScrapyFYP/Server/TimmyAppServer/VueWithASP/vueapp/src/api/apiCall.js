import axios from 'axios'


export default async (endpoint, method, query, params) => {
    let url = "https://localhost:7011"

    const requestHeader = {
        method: method,
        url: `${url}/api/${endpoint}`,
        data:{
            ...query
        },
        params:{
            ...params
        },
        headers:{
            'Content-Type': 'application/json',
        }
    }

    try {
        const response = await axios.request(requestHeader)
        const data = response.data
        // console.log(data)
        return data

    } catch (error) {
        console.log(error)
        throw error
    }
    
}