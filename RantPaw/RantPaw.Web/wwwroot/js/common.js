window.ShowSuccessfulAlert = (message) => {

    Swal.fire({
        position: 'top-end',
        icon: 'success',
        title: message,
        showConfirmButton: false,
        timer: 3000
    })

}
