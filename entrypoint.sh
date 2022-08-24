#!/bin/bash

# set -e
# run_cmd="dotnet run --server.urls http://*:80"

# until dotnet ef database update; do
# >&2 echo "SQL Server is starting up"
# sleep 1
# done

# >&2 echo "SQL Server is up - executing command"
# exec $run_cmd

# clean_dev_cert="dotnet dev-certs https --clean"
# add_dev_cert="dotnet dev-certs https"
# trust_dev_cert="dotnet dev-certs https --check --trust"


ln -s /lib/libc.musl-x86_64.so.1 /lib/ld-linux-x86-64.so.2
#exec $clean_dev_cert
#exec $add_dev_cert
#exec $trust_dev_cert

exec dotnet QnA.Demo.dll