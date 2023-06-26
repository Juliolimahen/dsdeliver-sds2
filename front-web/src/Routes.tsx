import { BrowserRouter, Switch, Route } from 'react-router-dom';
import { ThemeProvider, createTheme } from '@mui/material';
import Home from './components/Home';
import Navbar from './components/Navbar';
import Orders from './components/Orders/index';
import ProductList from './components/Cadastro/index';
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
