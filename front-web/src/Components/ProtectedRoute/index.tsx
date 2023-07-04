import React from 'react';
import { Route, Redirect, RouteProps } from 'react-router-dom';
import authService  from '../../Services/authService';

interface ProtectedRouteProps extends RouteProps {
  component: React.ComponentType<any>;
}

const ProtectedRoute: React.FC<ProtectedRouteProps> = ({ component: Component, ...rest }) => (
  <Route
    {...rest}
    render={(props) =>
      authService.isAuthenticated() ? (
        <Component {...props} />
      ) : (
        <Redirect to="/admin" />
      )
    }
  />
);

export default ProtectedRoute;
