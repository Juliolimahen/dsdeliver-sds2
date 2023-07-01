import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import {ThemeProvider} from '@mui/material';
import theme from './styles/theme/theme';
import './styles/App.css';
import Routes from './Routes';


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
