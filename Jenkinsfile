pipeline {
    agent any

    stages {
        stage('Clone Repository') {
            steps {
                git branch: 'main', url: 'https://github.com/khushboo0606/BookstoreWebApp.git'
            }
        }

        stage('Build') {
            steps {
                sh 'dotnet build'
            }
        }

        stage('Test') {
            steps {
                sh 'dotnet test'
            }
        }

        stage('Publish') {
            steps {
                sh 'dotnet publish -c Release -o out'
            }
        }

        stage('Docker Build') {
            steps {
                sh 'docker build -t bookstorewebapp .'
            }
        }

        stage('Docker Run') {
            steps {
                sh 'docker run -d -p 5000:80 --name bookstore-container bookstorewebapp || echo "Container may already exist"'
            }
        }
    }
}
