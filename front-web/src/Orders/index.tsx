import './styles.css';
import { ReactComponent as Logo} from './logo.svg';

function Orders (){
    return (
        <nav className="main-navbar">
            <Logo/>
            <a href="home" className="logo-text">DS Delivery</a>
        </nav>
    )
}

export default Orders;