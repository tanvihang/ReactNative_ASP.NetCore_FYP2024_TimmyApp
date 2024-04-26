import { createContext, useContext, useState, useEffect } from "react";

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
    const [spiders, setSpiders] = useState([])

    useEffect(() => {
        // check the use log in info, now just set it to no value, gonna check out how to get data from the mobile(like cookie)
        setUser("Angus Tan")
        setJwtToken("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiJVX2NlOWRlOWNkLWMxODMtNDA0ZS05MDBjLWQ3MmRhNGUxN2FhNSIsInVuaXF1ZV9uYW1lIjoidHZoIiwibmJmIjoxNzE0MDEwMDcxLCJleHAiOjE3MTQ2MTQ4NzEsImlhdCI6MTcxNDAxMDA3MX0.oBEKdwroZ-AMKyGVJBt5cTMRlw8ZKakezkaunwRcc10")
    
        // TODO replace this to api call when on dev
        setCategoryBrand({
            "statusCode": 200,
            "message": "Success",
            "data": {
              "categories": [
                "camera",
                "computer",
                "mobile",
                "tablet",
                "watches"
              ],
              "categoryBrands": {
                "camera": [
                  "canon",
                  "nikon",
                  "olympus"
                ],
                "computer": [
                  "apple"
                ],
                "mobile": [
                  "apple",
                  "asus",
                  "samsung"
                ],
                "tablet": [
                  "apple"
                ],
                "watches": [
                  "apple"
                ]
              }
            }
          })
        setModelDictionary({
            "statusCode": 200,
            "message": "Success",
            "data": {
              "products": {
                "camera": {
                  "canon": {
                    "canon": [
                      "eos 5d mark iv",
                      "eos 6d mark ii",
                      "eos r5"
                    ]
                  },
                  "nikon": {
                    "nikon": [
                      "d3500",
                      "d780",
                      "z7 ii"
                    ]
                  },
                  "olympus": {
                    "olympus": [
                      "epl2"
                    ]
                  }
                },
                "computer": {
                  "apple": {
                    "apple": [
                      "mac mini",
                      "macbook air",
                      "macbook pro"
                    ]
                  }
                },
                "mobile": {
                  "apple": {
                    "apple": [
                      "iphone 11",
                      "iphone 12",
                      "iphone 12 pro",
                      "iphone 12 pro max",
                      "iphone 13",
                      "iphone 13 pro",
                      "iphone 13 pro max",
                      "iphone 14",
                      "iphone 14 pro",
                      "iphone 14 pro max",
                      "iphone 15",
                      "iphone 15 pro",
                      "iphone 15 pro max",
                      "iphone 6s",
                      "iphone 7",
                      "iphone 8",
                      "iphone se",
                      "iphone x",
                      "iphone xr"
                    ]
                  },
                  "asus": {
                    "asus": [
                      "rog",
                      "rog phone 5",
                      "zenfone 8"
                    ]
                  },
                  "samsung": {
                    "samsung": [
                      "galaxy a52",
                      "galaxy note 20",
                      "galaxy s21"
                    ]
                  }
                },
                "tablet": {
                  "apple": {
                    "apple": [
                      "ipad 7",
                      "ipad 8",
                      "ipad 9",
                      "ipad air 4",
                      "ipad air 5",
                      "ipad air 6",
                      "ipad mini 4",
                      "ipad mini 5",
                      "ipad mini 6",
                      "ipad pro 3",
                      "ipad pro 4",
                      "ipad pro 5"
                    ]
                  }
                },
                "watches": {
                  "apple": {
                    "apple": [
                      "watch 6",
                      "watch se",
                      "watch series 1",
                      "watch series 2",
                      "watch series 3",
                      "watch series 4",
                      "watch series 5",
                      "watch series 6",
                      "watch series 7",
                      "watch series 8",
                      "watch series 9",
                      "watch series se"
                    ]
                  }
                }
              }
            }
          })
        setSpiders(["mudah", "aihuishou"])
        
    
    }, [])

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
                isLoading
            }}
        >
            {children}
        </GlobalContext.Provider>
    )
}

export default GlobalProvider;