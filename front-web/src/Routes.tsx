import { BrowserRouter, Switch, Route } from 'react-router-dom';
import { ThemeProvider, createTheme } from '@mui/material';
import Home from './Pages/Home/index';
import Navbar from './Components/Navbar/index';
import Orders from './Pages/Orders/index';
import ProductList from './Pages/Cadastro/index';
const theme = createTheme();


function Routes() {
  return (
    <ThemeProvider theme={theme}> {/* Adicione o ThemeProvider com o objeto de tema */}
      <BrowserRouter>
        <Navbar />
        <Switch>
          <Route path="/orders">
            <Orders />
          </Route>
          <Route path="/admin/products">
            <ProductList />
          </Route>
          <Route path="/">
            <Home />
          </Route>
        </Switch>
      </BrowserRouter>
    </ThemeProvider>
  );
}

export default Routes;
