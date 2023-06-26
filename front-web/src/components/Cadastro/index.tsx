import React, { useEffect, useState } from 'react';
import styled from 'styled-components';
import {
  Paper,
  TableContainer,
  Table,
  TableHead,
  TableRow,
  TableCell,
  TableBody,
  Modal,
  TextField,
  Button,
  useMediaQuery,
  Theme,
  createTheme,
  ThemeProvider
} from '@mui/material';
import Footer from '../Footer';
import { fetchProducts, saveProduct } from '../Orders/api';
import { Product } from '../Orders/types';
import StepsHeader from './StepsHeader';

const theme = createTheme();

const Container = styled.div<{ hasProducts: boolean }>`
  max-width: 830px;
  margin: 0 auto;
  padding: 20px;
  padding-top: 100px;
  display: flex;
  flex-direction: column;
  align-items: center;

  @media only screen and (max-width: 768px) {
    max-width: 100%;
    margin-bottom: 50px;
  }

  @media (max-height: 700px) {
    margin-bottom: 30px;
  }

  ${({ hasProducts }) => !hasProducts && `
    height: calc(100vh - 254px);
    justify-content: center;
  `}
`;


const ProductImage = styled.img`
  width: 100%;
  height: auto;
  margin-bottom: 5px;
  border-radius: 50%;
`;

const AddProductButtonWrapper = styled.div`
  display: flex;
  justify-content: center;
  margin-top: 20px;
`;

const StyledButton = styled(Button)`
  && {
    margin-bottom: 20px;
    background-color: var(--primary-color);
    color: white;
    &:hover {
      background-color: var(--primary-hover-color);
    }
  }
`;

const StyledModalPaper = styled(Paper) <{ theme: Theme }>`
  && {
    position: absolute;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
    width: 400px;
    padding: 24px;

    ${({ theme }) => theme.breakpoints.down('sm')} {
      width: 90%;
    }
  }
`;

const ProductList: React.FC = () => {
  const [products, setProducts] = useState<Product[]>([]);
  const [selectedProduct, setSelectedProduct] = useState<Product | null>(null);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [editedName, setEditedName] = useState('');
  const [editedPrice, setEditedPrice] = useState('');
  const [editedDescription, setEditedDescription] = useState('');
  const [editedImageUri, setEditedImageUri] = useState('');
  const [hasProducts, setHasProducts] = useState(false);

  useEffect(() => {
    fetchProducts()
      .then((response) => {
        setProducts(response.data);
        setHasProducts(response.data.length > 0); // Atualiza o estado hasProducts
      })
      .catch((error) => {
        console.error('Erro ao obter os produtos:', error);
      });
  }, []);

  const handleEditProduct = (product: Product) => {
    setSelectedProduct(product);
    setEditedName(product.name);
    setEditedPrice(product.price.toString());
    setEditedDescription(product.description);
    setEditedImageUri(product.imageUri);
    setIsModalOpen(true);
  };

  const handleModalClose = () => {
    setSelectedProduct(null);
    setEditedName('');
    setEditedPrice('');
    setEditedDescription('');
    setEditedImageUri('');
    setIsModalOpen(false);
  };

  const handleSaveChanges = async () => {
    if (!selectedProduct) return;

    const editedProduct: Product = {
      ...selectedProduct,
      name: editedName,
      price: parseFloat(editedPrice),
      description: editedDescription,
      imageUri: editedImageUri,
    };

    try {
      await saveProduct(editedProduct);
      setProducts((prevProducts) =>
        prevProducts.map((product) => (product.id === editedProduct.id ? editedProduct : product))
      );
      handleModalClose();
    } catch (error) {
      console.error('Erro ao editar o produto:', error);
    }
  };

  const handleAddProduct = async () => {
    const newProduct: Product = {
      id: products.length + 1,
      name: editedName,
      price: parseFloat(editedPrice),
      description: editedDescription,
      imageUri: editedImageUri,
    };

    try {
      await saveProduct(newProduct);
      setProducts((prevProducts) => [...prevProducts, newProduct]);
      handleModalClose();
    } catch (error) {
      console.error('Erro ao adicionar o produto:', error);
    }
  };

  const isMobile = useMediaQuery((theme: Theme) => theme.breakpoints.down('sm'));

  return (
    <ThemeProvider theme={theme}>
      <>
      <Container hasProducts={hasProducts}>
          <StepsHeader />
          <TableContainer component={Paper} sx={{ my: 4 }}>
            <Table>
              <TableHead>
                <TableRow>
                  <TableCell>Produto</TableCell>
                  <TableCell>Preço</TableCell>
                  <TableCell>Descrição</TableCell>
                  {!isMobile && <TableCell>Imagem</TableCell>}
                  <TableCell align="right">Ações</TableCell>
                </TableRow>
              </TableHead>
              <TableBody>
                {products.map((product) => (
                  <TableRow key={product.id}>
                    <TableCell component="th" scope="row">
                      {product.name}
                    </TableCell>
                    <TableCell>{product.price}</TableCell>
                    <TableCell>{product.description}</TableCell>
                    {!isMobile && (
                      <TableCell>
                        <ProductImage src={product.imageUri} alt={product.name} />
                      </TableCell>
                    )}
                    <TableCell align="right">
                      <Button variant="outlined" onClick={() => handleEditProduct(product)}>
                        Editar
                      </Button>
                    </TableCell>
                  </TableRow>
                ))}
              </TableBody>
            </Table>
          </TableContainer>
          <AddProductButtonWrapper>
            <StyledButton variant="contained" onClick={() => setIsModalOpen(true)}>
              Adicionar Produto
            </StyledButton>
          </AddProductButtonWrapper>

          <Modal open={isModalOpen} onClose={handleModalClose}>
            <StyledModalPaper theme={theme}>
              <TextField
                label="Nome"
                value={editedName}
                onChange={(e) => setEditedName(e.target.value)}
                fullWidth
                sx={{ mb: 2 }}
              />
              <TextField
                label="Preço"
                value={editedPrice}
                onChange={(e) => setEditedPrice(e.target.value)}
                fullWidth
                sx={{ mb: 2 }}
              />
              <TextField
                label="Descrição"
                value={editedDescription}
                onChange={(e) => setEditedDescription(e.target.value)}
                fullWidth
                multiline
                rows={4}
                sx={{ mb: 2 }}
              />
              <TextField
                label="Imagem"
                value={editedImageUri}
                onChange={(e) => setEditedImageUri(e.target.value)}
                fullWidth
                sx={{ mb: 2 }}
              />
              <StyledButton variant="contained" onClick={selectedProduct ? handleSaveChanges : handleAddProduct}>
                {selectedProduct ? 'Salvar Alterações' : 'Adicionar Produto'}
              </StyledButton>
            </StyledModalPaper>
          </Modal>
        </Container>
        <Footer />
      </>
    </ThemeProvider>
  );
};

export default ProductList;