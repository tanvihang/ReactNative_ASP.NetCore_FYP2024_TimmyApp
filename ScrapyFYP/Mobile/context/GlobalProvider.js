import { createContext, useContext, useState, useEffect } from "react";
import normalApiCall from "../hooks/normalApiCall";
import useFetch from "../hooks/useFetch";
const GlobalContext = createContext()
export const useGlobalContext = () => useContext(GlobalContext);

// wrap it inside a Provider, so anything inside can use the state
const GlobalProvider = ({children}) => {
   
    // global state
    const [isLoggedIn, setIsLoggedIn] = useState(false)
    const [jwtToken, setJwtToken] = useState(null)
    const [isLoading, setIsLoading] = useState(false)
    const [user, setUser] = useState(null)
    const [categoryBrand, setCategoryBrand] = useState(null)
    const [modelDictionary, setModelDictionary] = useState(null)
    const [category, setCategory] = useState("computer")
    const [spiders, setSpiders] = useState([])
    const [condition, setCondition] = useState([])
    const [userFavourite, setUserFavourite] = useState([])
    const [allModel, setAllModel] = useState()
    
    // global search state
    const [searchCategory, setSearchCategory] = useState(null)
    const [searchBrand, setSearchBrand] = useState(null)
    const [searchModel, setSearchModel] = useState(null)
    
    const [searchParams, setSearchParams] = useState({
      productSearchTerm: {
        category: "",
        brand: "",
        model: "",
        description: "",
        highest_price: 0,
        lowest_price: 0,
        country: "",
        state: "",
        condition: "",
        spider: [
          
        ],
        sort: "priceasc",
        isTest: 0
      },
      pageDTO: {
        pageSize: 10,
        currentPage: 1
      }
    })
    const [subscribeParams, setSubscribeParams] = useState({
      subscription_notification_method: "",
      subscription_notification_time: 0,
      category: "",
      brand: "",
      model: "",
      description: "",
      highest_price: 0,
      lowest_price: 0,
      country: "",
      state: "",
      condition: "",
      spider: [
        
      ]
    })

    const getUserInfo = async (jwtToken) => {
      try{
        const data = await normalApiCall("User/GetUserInfo","POST",{},{jwtToken: jwtToken})
        setUser(data.data)
      }catch(error){
        console.log(error)
      }
    }

    const getUserFavouriteIds = async (jwtToken) => {
      try{
        const data = await normalApiCall("UserFavourite/GetUserFavouriteIdList","POST",{},{jwtToken:jwtToken})
        setUserFavourite(data.data)
      }
      catch(error){
        console.log(error)
      }
    }

    const getAllModel = async() => {
      try{
        const data = await normalApiCall("TimmyProduct/GetAllAdoptedTimmyProductDict","GET",{},{})
        setAllModel(data.data)
      }
      catch(error){
        console.log(error)
      }
    }

    const getCategoryBrand = async () => {
      try{
        const data = await normalApiCall("TimmyProduct/GetCategoryBrandList","GET",{},{})
        setCategoryBrand(data.data)
      }
      catch(error){
        console.log(error)
      }
    }

    const getModelDictionary = async() => {
      try{
        const data = await normalApiCall("TimmyProduct/GetAllAdoptedTimmyProductDict","GET",{},{})
        setModelDictionary(data.data)
      }
      catch(error){
        console.log(error)
      }
    }


    useEffect(() => {

        // check the use log in info, now just set it to no value, gonna check out how to get data from the mobile(like cookie)
        // const jwtTokens = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiJVX2NlOWRlOWNkLWMxODMtNDA0ZS05MDBjLWQ3MmRhNGUxN2FhNSIsInVuaXF1ZV9uYW1lIjoidHZoIiwibmJmIjoxNzE2Mjg4ODIzLCJleHAiOjE3MTY4OTM2MjMsImlhdCI6MTcxNjI4ODgyM30.YUaR2rgMxkIp2dut9--OpVdd1cwSlLa_zbwNfuFGg8I"
        // setJwtToken(jwtToken)
        getUserInfo(jwtToken)    
        getCategoryBrand()
        getModelDictionary()
        getAllModel()

        setSpiders(["mudah", "aihuishou"])
        setCondition(["new", "mint", "used"])
        
        getUserFavouriteIds(jwtToken)
        

    }, [isLoggedIn])

    return (
        <GlobalContext.Provider
            value = {{
                isLoggedIn,
                setIsLoggedIn,
                jwtToken,
                setJwtToken,
                user,
                setUser,
                modelDictionary,
                categoryBrand,
                spiders,
                condition,
                userFavourite,
                setUserFavourite,
                isLoading,
                searchParams,
                setSearchParams,
                subscribeParams,
                setSubscribeParams,
                category,
                setCategory,
                searchCategory,
                setSearchCategory,
                searchBrand,
                setSearchBrand,
                allModel,
                searchModel,
                setSearchModel
            }}
        >
            {children}
        </GlobalContext.Provider>
    )
}

export default GlobalProvider;