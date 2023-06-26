import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import { createTheme, ThemeProvider, useMediaQuery} from '@mui/material';

import './App.css';
import Routes from './Routes';

const theme = createTheme({
  breakpoints: {
    values: {
      xs: 0,
      sm: 600,
      md: 960,
      lg: 1280,
      xl: 1920,
    },
  },
});


function App() {
  return (
    <>
    <ThemeProvider theme={theme}>
      <Routes />
      <ToastContainer />
      </ThemeProvider>
    </>
  );
}

export default App;
