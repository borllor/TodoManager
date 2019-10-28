# connect to Azure via Azure CLI
az login

# variables
Subscription="Free Trial"
AksResourceGroupName=aks-k8s-rg-10122
AksClusterName=aks-k8s-2

# Create resource group
az group create \
 --location "Australia East" \
 --subscription "$Subscription" \
 --name $AksResourceGroupName

# create AKS cluster
az aks create \
   --generate-ssh-keys \
   --subscription "$Subscription" \
   --node-count 2 \
   --resource-group $AksResourceGroupName \
   --name $AksClusterName

# connect to cluster
az aks get-credentials \
   --resource-group $AksResourceGroupName \
   --name $AksClusterName \
   --subscription "$Subscription"

# get access to Dashboard
kubectl create clusterrolebinding kubernetes-dashboard \
         --clusterrole=cluster-admin \
         --serviceaccount=kube-system:kubernetes-dashboard

# Open Dashboard
az aks browse \
    --resource-group $AksResourceGroupName \
    --name $AksClusterName \
    --subscription "$Subscription"