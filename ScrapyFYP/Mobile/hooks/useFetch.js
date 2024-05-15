import axios from 'axios'
import { Alert } from 'react-native'
import { useState, useEffect} from 'react'

export default useFetch = (endpoint,method,query,params) => {

    const [data, setData] = useState();
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState(null)    


    let url = "https://a68c-223-104-41-166.ngrok-free.app"

    // let data
    // create the request header


    const fetchData = async() => {

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
                'bypass-tunnel-reminder': "1234"
            }
        }

        console.log("--------------------")
        console.log(query)
        console.log("--------------------")

        setIsLoading(true)
        try {

            const response = await axios.request(requestHeader);
            if (response.data.statusCode == 200 ) {
                // Successful response
                setData(response.data)
                console.log(response.data)
                setIsLoading(false)
                console.log(response.data.message)
                // Alert.alert("Success", response.data.message)
            } else {
                // Error response
                setError(response.data.message)
                Alert.alert("Error", "Error status: " + response.data.message)
            }
        } catch (error) {
            // Axios request failed
            setIsLoading(false)
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

