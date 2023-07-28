import { ReactComponent as YouTubeIcon } from '../../assets/footer/youtube.svg';
import { ReactComponent as LikedinIcon } from '../../assets/footer/linkedin.svg';
import { ReactComponent as IntagramIcon } from '../../assets/footer/instagram.svg';
import { MainFooter, FooterIconLink, FooterIcons } from './style';

const Footer: React.FC = () => {
  return (
    <MainFooter>
      App desenvolvido durante a 2ª ed. do evento Semana DevSuperior e refatorado ao longo do tempo
      <FooterIcons>
        <FooterIconLink href="https://www.youtube.com/c/DevSuperior" target="_new">
          <YouTubeIcon />
        </FooterIconLink>
        <FooterIconLink href="https://www.linkedin.com/school/devsuperior/" target="_new">
          <LikedinIcon />
        </FooterIconLink>
        <FooterIconLink href="https://www.instagram.com/devsuperior.ig" target="_new">
          <IntagramIcon />
        </FooterIconLink>
      </FooterIcons>
    </MainFooter>
  );
}

export default Footer;
