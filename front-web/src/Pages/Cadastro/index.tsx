import React, { useEffect, useState } from 'react';
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
  ThemeProvider,
} from '@mui/material';
import Footer from '../../Components/Footer';
import { fetchProducts, saveProduct, createProduct, deleteProduct } from '../../Services/api';
import StepsHeader from './StepsHeader/index';
import { Product } from '../Orders/types';
import {
  Container,
  ButtonAlignmentStyle,
  ProductImage,
  AddProductButtonWrapper,
  StyledButton,
  StyledModalPaper,
} from './style';
import '../Orders/styles.css';

const theme = createTheme();

const ProductList: React.FC = () => {
  const [products, setProducts] = useState<Product[]>([]);
  const [selectedProduct, setSelectedProduct] = useState<Product | null>(null);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [editedName, setEditedName] = useState('');
  const [editedPrice, setEditedPrice] = useState('');
  const [editedDescription, setEditedDescription] = useState('');
  const [editedImageUri, setEditedImageUri] = useState('');
  const [hasProducts, setHasProducts] = useState(false);
  const [isEditing, setIsEditing] = useState(false);
  const [confirmDelete, setConfirmDelete] = useState(false);
  const [deleteConfirmationOpen, setDeleteConfirmationOpen] = useState(false);
  const [deleteProductId, setDeleteProductId] = useState<number | null>(null);

  useEffect(() => {
    fetchProducts()
      .then((response) => {
        setProducts(response.data);
        setHasProducts(response.data.length > 0);
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
    setIsEditing(true);
  };

  const handleDeleteProduct = async (productId: number) => {
    if (confirmDelete) {
      try {
        await deleteProduct(productId);
        setProducts((prevProducts) => prevProducts.filter((product) => product.id !== productId));
        setConfirmDelete(false);
      } catch (error) {
        console.error('Erro ao excluir o produto:', error);
      }
    } else {
      setDeleteProductId(productId);
      setDeleteConfirmationOpen(true);
    }
  };

  const handleConfirmDelete = async () => {
    if (deleteProductId) {
      try {
        await deleteProduct(deleteProductId);
        setProducts((prevProducts) => prevProducts.filter((product) => product.id !== deleteProductId));
        setDeleteConfirmationOpen(false);
      } catch (error) {
        console.error('Erro ao excluir o produto:', error);
      }
    }
  };

  const handleCancelDelete = () => {
    setDeleteConfirmationOpen(false);
    setDeleteProductId(null);
  };

  const handleModalClose = () => {
    setSelectedProduct(null);
    setEditedName('');
    setEditedPrice('');
    setEditedDescription('');
    setEditedImageUri('');
    setIsModalOpen(false);
    setIsEditing(false);
    setConfirmDelete(false);
  };

  const handleSaveChanges = async () => {
    if (!editedName || !editedPrice || !editedDescription || !editedImageUri) return;

    const editedProduct: Product = {
      id: selectedProduct ? selectedProduct.id : 0,
      name: editedName,
      price: parseFloat(editedPrice),
      description: editedDescription,
      imageUri: editedImageUri,
    };

    try {
      if (isEditing) {
        await saveProduct(editedProduct);
        setProducts((prevProducts) =>
          prevProducts.map((product) => (product.id === editedProduct.id ? editedProduct : product))
        );
      } else {
        await createProduct(editedProduct);
        setProducts((prevProducts) => [...prevProducts, editedProduct]);
      }

      handleModalClose();
    } catch (error) {
      console.error('Erro ao salvar as alterações:', error);
    }
  };

  const handleAddProduct = () => {
    setSelectedProduct(null);
    setEditedName('');
    setEditedPrice('');
    setEditedDescription('');
    setEditedImageUri('');
    setIsModalOpen(true);
    setIsEditing(false);
  };

  const isMobile = useMediaQuery((theme: Theme) => theme.breakpoints.down('sm'));

  return (
    <ThemeProvider theme={theme}>
      <>
        <StepsHeader />
        <Container hasProducts={hasProducts}>
          <TableContainer component={Paper} sx={{ my: 4, maxWidth: 830 }}>
            <Table>
              <TableHead>
                <TableRow>
                  <TableCell>Produto</TableCell>
                  <TableCell>Preço</TableCell>
                  <TableCell>Descrição</TableCell>
                  {!isMobile && <TableCell>Imagem</TableCell>}
                  <TableCell align="center">Ações</TableCell>
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
                      <ButtonAlignmentStyle>
                        <Button
                          sx={{ maxWidth: 80 }}
                          variant="outlined"
                          onClick={() => handleEditProduct(product)}
                        >
                          Editar
                        </Button>
                        <Button
                          sx={{ maxWidth: 80 }}
                          variant="outlined"
                          onClick={() => handleDeleteProduct(product.id)}
                        >
                          {confirmDelete ? 'Confirmar' : 'Excluir'}
                        </Button>
                      </ButtonAlignmentStyle>
                    </TableCell>
                  </TableRow>
                ))}
              </TableBody>
            </Table>
          </TableContainer>
          <AddProductButtonWrapper>
            <StyledButton variant="contained" onClick={handleAddProduct}>
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
              <StyledButton variant="contained" onClick={handleSaveChanges}>
                {isEditing ? 'Salvar Alterações' : 'Adicionar Produto'}
              </StyledButton>
            </StyledModalPaper>
          </Modal>
          <Modal open={deleteConfirmationOpen} onClose={handleCancelDelete}>
            <StyledModalPaper sx={{ maxWidth: 280 }} theme={theme}>
              <p>Deseja realmente excluir o produto?</p>
              <StyledButton variant="contained" onClick={handleConfirmDelete}>
                Confirmar
              </StyledButton>
              <StyledButton sx={{ marginLeft: 5 }} variant="contained" onClick={handleCancelDelete}>
                Cancelar
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
