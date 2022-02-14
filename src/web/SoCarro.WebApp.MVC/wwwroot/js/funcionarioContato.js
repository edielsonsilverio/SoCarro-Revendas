

//$("#btnExcluir").click(function (e) {
//    e.preventDefault();
 
//    Delete(4);
//}




function Delete(data) {
    swalWithBootstrapButtons.fire({
        titleText: "Excluir Registro",
        title: "Deseja excluir o registro?",
        text: "Você não poderá restaurar o conteúdo!",
        icon: 'warning',
        showCancelButton: true,
        cancelButtonColor: "#3E4492",
        titleColor: "#3E4492",
        cancelButtonText: "Não",
        confirmButtonColor: "#3E4492",
        confirmButtonText: "Sim",
        closeOnconfirm: true,
        reverseButtons: true
    }).then((result) => {
        if (result.value) {//Se apertou sim

            $.ajax({
                type: 'POST',
                url: "/FuncionarioContato/DeletePost/${data}",
                success: function (data) {

                    if (data.success) {
                        toastr.success(data.message);
                        swalWithBootstrapButtons.fire(
                            'Registro excluído com sucesso.',
                            'Exclusão de Registro!',
                            'success'
                        )
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });

        } else {//Apertou não
            //swalWithBootstrapButtons.fire(
            //    'Operação cancelada.',
            //    'Exclusão de Registro',
            //    'error'
            //)
            //Termina o Alerta de Não
        }//Finaliza o Senão


    });//Termina o [then]
}//Termina a função
