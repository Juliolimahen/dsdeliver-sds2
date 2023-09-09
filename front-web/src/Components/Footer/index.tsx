import { ReactComponent as YouTubeIcon } from '../../assets/footer/youtube.svg';
import { ReactComponent as LikedinIcon } from '../../assets/footer/linkedin.svg';
import { ReactComponent as IntagramIcon } from '../../assets/footer/instagram.svg';
import { MainFooter, FooterIconLink, FooterIcons } from './style';

const Footer: React.FC = () => {
  return (
    <MainFooter>
      App desenvolvido durante a 2ª ed. do evento Semana DevSuperior e refatorado ao longo do tempo pelo autor.
      <FooterIcons>
        <FooterIconLink href="#" target="_new">
          <YouTubeIcon />
        </FooterIconLink>
        <FooterIconLink href="https://www.linkedin.com/in/julio-henrique-143193154" target="_new">
          <LikedinIcon />
        </FooterIconLink>
        <FooterIconLink href="#" target="_new">
          <IntagramIcon />
        </FooterIconLink>
      </FooterIcons>
    </MainFooter>
  );
}

export default Footer;
