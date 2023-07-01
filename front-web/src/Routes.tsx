
import { BrowserRouter, Switch, Route } from 'react-router-dom';
import { ThemeProvider} from '@mui/material';
import Home from './Pages/Home/index';
import Navbar from './Components/Navbar/index';
import Orders from './Pages/Orders/index';
import ProductList from './Pages/Cadastro/index';
import Login from './Pages/Admin/Login';
import theme from './styles/theme/theme';
import Footer from './Components/Footer';

const Routes: React.FC = () => {
  return (
    <ThemeProvider theme={theme}>
      <BrowserRouter>
        <Navbar />
        <Switch>
          <Route path="/orders">
            <Orders />
          </Route>
          <Route path="/admin/products">
            <ProductList />
          </Route>
          <Route path="/" exact>
            <Home />
          </Route>
          <Route path="/admin">
            <Login />
          </Route>
        </Switch>
        <Footer/>
      </BrowserRouter>
    </ThemeProvider>
  );
};

export default Routes;

