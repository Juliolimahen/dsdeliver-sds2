import { ReactComponent as Logo } from '../../assets/navbar/logo.svg';
import { MainNavbar, LogoText} from './style';

function Navbar() {
  return (
    <MainNavbar>
      <Logo />
      <LogoText to="/">DS Delivery</LogoText>
    </MainNavbar>
  );
}

export default Navbar;
