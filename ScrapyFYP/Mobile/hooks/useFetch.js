import axios from 'axios'
import { Alert } from 'react-native'
import { useState, useEffect} from 'react'

const useFetch = (endpoint,method,query,params) => {

    const [data, setData] = useState();
    const [isLoading, setIsLoading] = useState(false);
    const [error, setError] = useState(null)    

    let url = "https://d126-114-246-192-222.ngrok-free.app"

    // let data
    // create the request header
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
            "ngrok-skip-browser-warning": "69420",
            'Content-Type': 'application/json',
        }
    }

    const fetchData = async() => {
        try {

            setIsLoading(true)
            const response = await axios.request(requestHeader);
            if (response.data.statusCode == 200 ) {
                // Successful response
                setData(response.data.data)
                setIsLoading(false)
                console.log(response.data.message)
                Alert.alert("Success", response.data.message)
            } else {
                // Error response
                setError(response.data.message)
                setIsLoading(false)
                Alert.alert("Error", "Error status: " + response.data.message)
            }
        } catch (error) {
            // Axios request failed
            setError(error)
            alert(`Error while fetching URL: ${error}`);
        }
    };    

    useEffect(()=>{
        fetchData()
    }, [])

    const refetch = () => {
        setIsLoading(true);
        fetchData()
    }

    return {data, isLoading, error, refetch};
}

export default useFetch