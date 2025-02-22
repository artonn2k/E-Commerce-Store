import { Container, createTheme, CssBaseline, ThemeProvider } from "@mui/material";
import { useCallback, useEffect, useState } from "react";
import { Outlet, useLocation } from "react-router-dom";
import { ToastContainer } from "react-toastify";
import Header from "./Header";
import 'react-toastify/dist/ReactToastify.css';
import LoadingComponent from "./LoadingComponent";
import { useAppDispatch } from "../store/configureStore";
import { fetchBasketAsync } from "../../features/basket/basketSlice";
import { fetchCurrentUser } from "../../features/account/accountSlice";
import HomePage from "../../features/home/HomePage";




function App() {
  const location = useLocation();
  const dispatch = useAppDispatch();
  const [loading, setLoading] = useState(true);

  //created this iniApp func beacuse useEffect was to messy boilerplate
  const initApp = useCallback(async () => {
    try{
      await dispatch(fetchCurrentUser());
      await dispatch(fetchBasketAsync());
    }catch(error){
      console.log(error);
    }
  }, [dispatch])
  
  useEffect(() => {
    initApp().then(() => setLoading(false));
  }, [initApp])

  //the switch for darkmode
  const [darkMode, setDarkMode] = useState(false);
  const paletteType = darkMode ? 'dark' : 'light';

  const theme = createTheme({
    palette: {
      mode: paletteType,
      background: {
        default: paletteType === 'light'? '#eaeaea' : '#121212'
      }
    }
  })

  function handleThemeChange() {
    setDarkMode(!darkMode);
  }

  if(loading) return <LoadingComponent  message='Initialising app....'/>

  return (
    <ThemeProvider theme={theme}>
      <ToastContainer position="bottom-right" theme="colored" hideProgressBar />
      <CssBaseline />
      <Header darkMode={darkMode} handleThemeChange={handleThemeChange} />
      {loading ? <LoadingComponent message="Initializing app...."/> 
          :location.pathname ==='/' ? <HomePage />
          : <Container sx={{mt: 4}}>
              <Outlet />
            </Container>
      }

      
    </ThemeProvider>
  );
}

export default App;
