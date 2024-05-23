import { View, Text, TouchableOpacity, Image } from 'react-native'
import {React, useState, useEffect} from 'react'
import { useGlobalContext } from '../../context/GlobalProvider'
import {images, icons} from "../../constants"
import normalApiCall from '../../hooks/normalApiCall'

const FavouriteButton = ({productId}) => {

    const {userFavourite, setUserFavourite, jwtToken} = useGlobalContext()
   
    const [favourite, setFavourite] = useState(false)
    
    // set the color
    useEffect(() => {
        if(userFavourite.indexOf(productId) != -1){
            setFavourite(true)
        }
    },[])

    const favouriteApi = () => {
        try{
            normalApiCall("UserFavourite/FavouriteProduct", "POST", {productUniqueId:productId} ,{jwtToken : jwtToken})
            // setUserFavourite(userFavourite.push(productId))
            setUserFavourite(userFavourite.concat([productId]))
            
        }catch(error){
            console.log(error)
        }
        
    }

    const unFavouriteApi = () => {
        normalApiCall("UserFavourite/UnFavouriteProduct", "POST", {productUniqueId:productId} ,{jwtToken : jwtToken})

        // Remove it from userFavourite
        let uf = userFavourite
        let index = uf.indexOf(productId)
        if(index > -1){
            uf.splice(index,1)
        }
        setUserFavourite(uf)
    }

    const userPressFavourite = () => {
        if(favourite == false){
            const data = favouriteApi()
            setFavourite(true)
        }else{
            const data = unFavouriteApi()
            setFavourite(false)
        }
    }


  return (
    <TouchableOpacity 
    onPress={() =>
        {
            userPressFavourite()
        }}
    >
        <Image
            source={icons.favourite}
            className = "w-8 h-8 ml-4"
            tintColor= {`${(favourite ? "#FFD700" : "#000000")}`}
        />
    </TouchableOpacity>
  )
}

export default FavouriteButton